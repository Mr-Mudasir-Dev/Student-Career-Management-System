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
            IMapper mapper, ILogger<IdentityRepository> logger, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _emailService = emailService;
        }


        //GetRoles Method
        public async Task<IList<string>> GetRoles(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(appUser!);
            return roles;
        }


        //Login Method
        public async Task<LoginOpretionResult<User>> Login(string identifier, string password)
        {
            var currentUser = await _userManager.FindByEmailAsync(identifier)
                    ?? await _userManager.FindByNameAsync(identifier);
            if (currentUser == null)
                return LoginOpretionResult<User>.Failure("Invalid credentials");
            if (!currentUser.EmailConfirmed)
                return LoginOpretionResult<User>.Failure("Please Confirm Your Email");
            var result = await _userManager.CheckPasswordAsync(currentUser, password);
            if (!result) return LoginOpretionResult<User>.Failure("Invalid credentials");
            var appUser = _mapper.Map<User>(currentUser);
            return LoginOpretionResult<User>.Success(appUser);
        }


        //Register Method

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
            //Create User In ASPNetUser
            var result = await _userManager.CreateAsync(appUser, password);

            if (result.Succeeded)
            {
                //Add Role To User
                await _userManager.AddToRoleAsync(appUser, "User");

                _logger.LogInformation($"Identity Repository :User {appUser.UserName} registered successfully. Sending verification email.");
                // Generate email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                _logger.LogInformation($"Identity Repository :Generated email confirmation token for user {appUser.UserName}: {token}");

                // Encode the token for URL usage
                var encodedToken = HttpUtility.UrlEncode(token);
                _logger.LogInformation($"Identity Repository :Encoded email confirmation token for user {appUser.UserName}: {encodedToken}");

                // verify the token by decoding it
                var baseUrl = _configuration["AppSettings:BaseUrl"];
                _logger.LogInformation($"Base URL for email verification: {baseUrl}");

                var confirmationLink = $"{baseUrl}/api/Authentication/verify-email?token={encodedToken}&email={appUser.Email}";
                _logger.LogInformation($"confirmationLink {confirmationLink}", confirmationLink);
                var EmailSender = await _emailService.SendVerificationEmailAsync(appUser.Email!, confirmationLink);
                if (EmailSender)
                {
                    _logger.LogInformation($"Identity Repository : Verification email sent to {appUser.Email} for user {appUser.UserName}.");
                    return IdentityOperationResult.Success();
                }

            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return IdentityOperationResult.Failure(errors);
        }




        //VerifyEmail Method
        public async Task<IdentityOperationResult> VerifyEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return IdentityOperationResult.Failure(new List<string> { "User not found" });
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return IdentityOperationResult.Success();
            }
            var errors = result.Errors.Select(e => e.Description).ToList();
            return IdentityOperationResult.Failure(errors);
        }
    }
}
