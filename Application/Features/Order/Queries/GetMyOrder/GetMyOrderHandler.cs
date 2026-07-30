using Application.Common;
using Application.Features.Order.Queries.DTOs;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries.GetMyOrder
{
    public class GetMyOrderHandler : IRequestHandler<GetMyOrderQuery, Result<List<OrderDto>>>
    {
        private readonly IUnitOfWork _uow;
        public GetMyOrderHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<OrderDto>>> Handle(GetMyOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepository
                .GetByUserIdAsync(request.UserId, cancellationToken);

            if (!order.Any())
                return Result<List<OrderDto>>.Success(new List<OrderDto>(), "You have not any order!");

            var dto = order.Select(o => new OrderDto
            {
                Id = o.Id,
                ShippingAddress = o.ShippingAddress,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                CreatedAt = o.CreatedAt,
                Items = o.OrderItems.Select(oi => new OrderItemDto
                {
                    BookId = oi.BookId,
                    BookTitle = oi.Book.Title,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()

            }).ToList();

            return Result<List<OrderDto>>.Success(dto, "Order feched successfully!");
        }
    }
}
