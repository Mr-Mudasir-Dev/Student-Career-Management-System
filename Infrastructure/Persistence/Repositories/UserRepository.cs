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
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public UserRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }



        public async Task<bool> DeleteUser(string Id)
        {
            var exestingUser = await _userManager.FindByIdAsync(Id);
            if (exestingUser == null) return false;
            var deleteUser = await _userManager.DeleteAsync(exestingUser);
            return deleteUser.Succeeded;
        }

        public async Task<bool> EditUser(User user)
        {
            if (string.IsNullOrEmpty(user.Id))
                return false;

            var exestingUser = await _userManager.FindByIdAsync(user.Id);
            if (exestingUser == null) return false;
            _mapper.Map(user, exestingUser);
            var update = await _userManager.UpdateAsync(exestingUser);
            return update.Succeeded;
        }

        public async Task<bool> ExistsByUserIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId) != null;
        }

        public async Task<User?> GetByEmail(string Email)
        {
            ApplicationUser? exestingUser = 
                await _userManager.FindByEmailAsync(Email);
            if (exestingUser == null) return null;
            return _mapper.Map<User>(exestingUser);
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if(user == null) return null;
            return _mapper.Map<User>(user);
        }
    }
}
