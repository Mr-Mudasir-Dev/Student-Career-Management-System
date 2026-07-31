using Application.Common;
using Application.Features.Order.Queries.DTOs;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries.GetOrderById
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
    {
        private readonly IUnitOfWork _uow;
        public GetOrderByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepository
                .GetByIdWithItemsAsync(request.Id, cancellationToken);

            if (order == null)
                throw new NotFoundException("Order", request.Id);

            var dto = new OrderDto
            {
                Id = order.Id,
                ShippingAddress = order.ShippingAddress,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    BookId = oi.BookId,
                    BookTitle = oi.Book.Title,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };

            return Result<OrderDto>.Success(dto, "Orders fetched successfully!");
        }
    }
}
