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
        Task<IdentityOperationResult> Register(User user,  string password);
        Task<IdentityOperationResult> Login(User user, string password);
        Task<IList<string>> GetRoles(string id);
    }
}
