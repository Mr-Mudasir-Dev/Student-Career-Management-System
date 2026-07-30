using Application.Interface.Repository;
using Domain.Entities;
using Domain.Enums.Order;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllWithItemsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)

                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)

                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetByIdWithItemsAsync(int Id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)

                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)

                .FirstOrDefaultAsync(o => o.Id == Id, cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)

                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)

                .Where(o => o.Status == status)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(cancellationToken);

        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(string UserId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)

                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)

                .Where(o => o.UserId == UserId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
