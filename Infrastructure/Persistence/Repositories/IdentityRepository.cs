using Application.Common;
using Application.Features.Authentication.Command.Login;
using Application.Interface.Repository;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Persistence.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<IdentityRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public IdentityRepository(UserManager<ApplicationUser> userManager,
            IMapper mapper, ILogger<IdentityRepository> logger,IConfiguration configuration,IEmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<IList<string>> GetRoles(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(appUser!);
            return roles;
        }

        public async Task<LoginOpretionResult<User>> Login(string identifier, string password)
        {
            var currentUser = await _userManager.FindByEmailAsync(identifier)
                    ?? await _userManager.FindByNameAsync(identifier);
            if (currentUser == null)
            {
                return LoginOpretionResult<User>.Failure("Invalid credentials");
            }
            var result = await _userManager.CheckPasswordAsync(currentUser, password);
            if (!result) return LoginOpretionResult<User>.Failure("Invalid credentials");
            var appUser = _mapper.Map<User>(currentUser);
            return LoginOpretionResult<User>.Success(appUser);
        }
        public async Task<IdentityOperationResult> Register(User user, string password)
        {
            var appUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Age = user.Age,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(appUser, password);
            if (result.Succeeded)

            {
                await _userManager.AddToRoleAsync(appUser, "User");
                // Generate email confirmation token
                _logger.LogInformation($"Identity Repository :User {appUser.UserName} registered successfully. Sending verification email.");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                // Encode the token for URL usage
                _logger.LogInformation($"Identity Repository :Generated email confirmation token for user {appUser.UserName}: {token}");
                var encodedToken = HttpUtility.UrlEncode(token);
                // verify the token by decoding it
                _logger.LogInformation($"Identity Repository :Encoded email confirmation token for user {appUser.UserName}: {encodedToken}");
                var baseUrl = _configuration["AppSettings:BaseUrl"];

                _logger.LogInformation($"Base URL for email verification: {baseUrl}");
                var confirmationLink = $"{baseUrl}/api/auth/verify-email?token={encodedToken}&email={appUser.Email}";
                _logger.LogInformation($"Confirmation link for user {appUser.UserName}: {confirmationLink}");
               var EmailSender =  await _emailService.SendVerificationEmailAsync(appUser.Email!, confirmationLink);
                if (EmailSender)
                { 
                _logger.LogInformation($"Identity Repository : Verification email sent to {appUser.Email} for user {appUser.UserName}.");
                    return IdentityOperationResult.Success();
                }

            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return IdentityOperationResult.Failure(errors);
        }

        public async Task<IdentityOperationResult> VerifyEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return IdentityOperationResult.Failure(new List<string> { "User not found" });
            }
            // Decode the token
            var decodedToken = HttpUtility.UrlDecode(token);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
            {
                return IdentityOperationResult.Success();
            }
            var errors = result.Errors.Select(e => e.Description).ToList();
            return IdentityOperationResult.Failure(errors);
        }
    }
}
