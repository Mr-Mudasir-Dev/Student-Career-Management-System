using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<IEnumerable<Review>> GetAllWithDetailAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Review>> GetByBookIdAsync(int BookId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Review>> GetByUserIdAsync(string UserId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string UserId, int bookId, int orderId, CancellationToken cancellationToken = default);
    }
}
