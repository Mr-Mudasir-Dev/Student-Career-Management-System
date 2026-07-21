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
        IBookRepository BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IGenreRepository GenreRepository { get; }
        IIdentityRepository IdentityRepository { get; }
        IUserRepository UserRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
