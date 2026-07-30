using Application.Interface.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly AppDbContext _context;
        public ReviewRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<bool> ExistsAsync(string UserId, int bookId, int orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
                .AnyAsync(r => r.UserId == UserId &&
                r.BookId == bookId &&
                r.OrderId == orderId, cancellationToken);
        }

        public async Task<IEnumerable<Review>> GetAllWithDetailAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
                .Include(r => r.Book)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Review>> GetByBookIdAsync(int BookId, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
                .Where(r => r.BookId == BookId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Review>> GetByUserIdAsync(string UserId, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
                .Include(r => r.Book)
                .Where(r => r.UserId == UserId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
