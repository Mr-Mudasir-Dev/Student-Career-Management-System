using Application.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        IIdentityRepository IdentityRepository { get; }
        IUserRepository UserRepository { get; }

<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes
    }
}
