using Application.Common;
using Application.Features.Authentication.Command.Login;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IIdentityRepository
    {
        Task<IdentityOperationResult> Register(User user, string password);
        Task<LoginOpretionResult<User>> Login(string identifier, string password);
        Task<IList<string>> GetRoles(string id);
        
        Task<IdentityOperationResult> VerifyEmail(string token, string email);

        Task<IdentityOperationResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    }
}
