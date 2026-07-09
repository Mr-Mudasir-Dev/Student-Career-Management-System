using Application.Common;
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
        public IdentityRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
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
