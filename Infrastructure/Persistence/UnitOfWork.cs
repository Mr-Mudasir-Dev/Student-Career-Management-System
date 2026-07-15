using Application.Interface;
using Application.Interface.Repository;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public IIdentityRepository IdentityRepository { get; }
        public IUserRepository UserRepository { get; }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        

        private readonly AppDbContext _context;
        public UnitOfWork(
            AppDbContext context,
            IIdentityRepository identityRepository,
<<<<<<< Updated upstream
            IUserRepository userRepository)
=======
            IUserRepository userRepository
            )
>>>>>>> Stashed changes
        {
            _context = context;
            IdentityRepository = identityRepository;
            UserRepository = userRepository;
<<<<<<< Updated upstream
=======
            
>>>>>>> Stashed changes
        }

        

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
