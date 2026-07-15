using Application.Common;
using Application.Features.Authentication.Command.Login;
using Application.Interface.Repository;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
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

        public IdentityRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IList<string>> GetRoles(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            return await _userManager.GetRolesAsync(appUser!);
        }

        public async Task<IdentityOperationResult> Login(User user, string password)
        {
            var currentUser = _mapper.Map<ApplicationUser>(user);
            var result = await _signInManager.PasswordSignInAsync(currentUser, password, false, false);
            return !result.Succeeded ? IdentityOperationResult.Failure(new List<string> { "Login failed." }) :  IdentityOperationResult.Success();

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
                return IdentityOperationResult.Success();

            var errors = result.Errors.Select(e => e.Description).ToList();
            return IdentityOperationResult.Failure(errors);
        }

        
    }
}
