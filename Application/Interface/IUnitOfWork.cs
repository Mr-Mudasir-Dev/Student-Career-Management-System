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
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


        IIdentityRepository IdentityRepository { get; }
        IUserRepository UserRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }

        IFindUserRepository FindUserRepository { get; }

    }
}
