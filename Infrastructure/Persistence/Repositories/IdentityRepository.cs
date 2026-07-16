using Application.Common;
using Application.Features.Authentication.Command.Login;
using Application.Interface.Repository;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<IdentityRepository> _logger;

        public IdentityRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, ILogger<IdentityRepository> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IList<string>> GetRoles(string id)
        {
            _logger.LogInformation("Fetching roles for user with ID: {UserId}", id);
            var appUser = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(appUser!);
            _logger.LogInformation($"user Role Count: {roles.Count}");
            return roles;
        }

        public async Task<LoginOpretionResult<User>> Login(string identifier, string password)
        {
            var currentUser = await _userManager.FindByEmailAsync(identifier)
                    ?? await _userManager.FindByNameAsync(identifier);
            if (currentUser == null)
            {
                return LoginOpretionResult<User>.Failure("Invalid Credintials");
            }
            var result = await _userManager.CheckPasswordAsync(currentUser, password);
            if (!result) return LoginOpretionResult<User>.Failure("Invalid Credintials");
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
                return IdentityOperationResult.Success();
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return IdentityOperationResult.Failure(errors);
        }
    }
}
