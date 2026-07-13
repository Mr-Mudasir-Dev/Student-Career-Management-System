using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IFindUserRepository
    {
        Task<User?> FindByEmailOrUsernameAsync(string identifier);
    }
}
