using Application.Common;
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
<<<<<<< Updated upstream
=======
        Task<LoginOpretionResult<User>> Login(string identifier, string password);
        Task<IList<string>> GetRoles(string id);
>>>>>>> Stashed changes
    }
}
