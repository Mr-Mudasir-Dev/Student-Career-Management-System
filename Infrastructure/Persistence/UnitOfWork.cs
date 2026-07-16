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
        public IFeedbackRepository FeedbackRepository { get; }
        public IFindUserRepository FindUserRepository { get; }




        private readonly AppDbContext _context;
        public UnitOfWork(
            AppDbContext context,
            IIdentityRepository identityRepository,
            IUserRepository userRepository,
            IFeedbackRepository feedbackRepository,
            IFindUserRepository findUserRepository)
       
        {
            _context = context;
            IdentityRepository = identityRepository;
            UserRepository = userRepository;
            FeedbackRepository = feedbackRepository;
            FindUserRepository = findUserRepository;
        }

        

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           return await _context.SaveChangesAsync();
        }
    }
}
