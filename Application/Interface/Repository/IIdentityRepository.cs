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
    }
}
