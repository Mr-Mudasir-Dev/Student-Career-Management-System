using Application.Interface.Repository;
using Domain.Entities;
using Domain.Enums.Feedback;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private readonly AppDbContext _context;
        public FeedbackRepository(AppDbContext context) :base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feedback>> GetByCategoryAync(FeedbackCategory category, CancellationToken cancellationToken = default)
        {
            return await _context.Feedbacks
                .Where(f => f.Category == category)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Feedback>> GetByStatusAsync(FeedbackStatus status, CancellationToken cancellationToken = default)
        {
            return await _context.Feedbacks
                .Where(f => f.Status == status)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Feedback>> GetByUseridAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.Feedbacks
                .Where(f => f.UserId == id)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
