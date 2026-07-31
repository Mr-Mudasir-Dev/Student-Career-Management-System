using Domain.Entities;
using Domain.Enums.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllWithItemsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetByUserIdAsync(string UserId, CancellationToken cancellationToken = default);
        Task<Order?> GetByIdWithItemsAsync(int Id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status, CancellationToken cancellationToken = default);
    }
}
