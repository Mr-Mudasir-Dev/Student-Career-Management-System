using Application.Interface;
using Application.Interface.Repository;
using Infrastructure.Data;
using Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public IReviewRepository ReviewRepository { get;}
        public IOrderRepository OrderRepository { get; }
        public IBookRepository BookRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public IGenreRepository GenreRepository { get; }
        public IIdentityRepository IdentityRepository { get; }
        public IUserRepository UserRepository { get; }
        public IFeedbackRepository FeedbackRepository { get; }



        private readonly AppDbContext _context;
        public UnitOfWork(
            AppDbContext context,
            IReviewRepository reviewRepository,
            IOrderRepository orderRepository,
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository,
            IIdentityRepository identityRepository,
            IUserRepository userRepository,
            IFeedbackRepository feedbackRepository)
        {
            _context = context;
            ReviewRepository = reviewRepository;
            OrderRepository = orderRepository;
            BookRepository = bookRepository;
            AuthorRepository = authorRepository;
            GenreRepository = genreRepository;
            IdentityRepository = identityRepository;
            UserRepository = userRepository;
            FeedbackRepository = feedbackRepository;
        }


        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync();
        }
    }
}
