using Application.Interface.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBestsellersAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Where(b => b.IsBestseller && b.IsAvailable)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId, CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Where(b => b.BookAuthors.Any(ba => ba.AuthorId == authorId))
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId, CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Where(b => b.BookGenres.Any(bg => bg.GenreId == genreId))
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetNewArrivalsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Where(b => b.IsNewArrival && b.IsAvailable)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
