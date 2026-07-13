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
    public class FindUserRepository : IFindUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public FindUserRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<User?> FindByEmailOrUsernameAsync(string identifier)
        {
            var applicationUser = await _userManager.FindByEmailAsync(identifier)
                ?? await _userManager.FindByNameAsync(identifier);

            return applicationUser != null ? _mapper.Map<User>(applicationUser) : null;

        }
    }
}
