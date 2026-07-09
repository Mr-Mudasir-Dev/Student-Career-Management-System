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
        

        private readonly AppDbContext _context;
        public UnitOfWork(
            AppDbContext context,
            IIdentityRepository identityRepository,
            IUserRepository userRepository)
        {
            _context = context;
            IdentityRepository = identityRepository;
            UserRepository = userRepository;
        }

        

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
