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

        public async Task<bool> ExistsByTitleAsync(string title, CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .AnyAsync(b => b.Title.ToLower() == title.ToLower(), cancellationToken);

        }

        public async Task<IEnumerable<Book>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Where(b => b.IsAvailable)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetBestsellersAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Where(b => b.IsBestseller && b.IsAvailable)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId, CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Where(b => b.BookAuthors.Any(ba => ba.AuthorId == authorId))
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId, CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Where(b => b.BookGenres.Any(bg => bg.GenreId == genreId))
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Book?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetNewArrivalsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Where(b => b.IsNewArrival && b.IsAvailable)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetSearchByTitleAsync(string serachTerm, CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Where(b => 
                b.Title.Contains(serachTerm) || 
                b.BookAuthors.Any(ba => ba.Author.Name.Contains(serachTerm)) ||
                b.BookGenres.Any(bg => bg.Genre.Name.Contains(serachTerm)))
                .Where(b => b.IsAvailable)
                .OrderBy(b => b.Title)
                .ToListAsync(cancellationToken);
        }
    }
}
