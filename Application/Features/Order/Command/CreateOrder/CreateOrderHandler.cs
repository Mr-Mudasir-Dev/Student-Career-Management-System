using Application.Common;
using Application.Interface;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Command.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        public CreateOrderHandler(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach(var item in request.Items)
            {
                var book = await _uow.BookRepository.GetByIdAsync(item.BookId, cancellationToken);

                if (book == null)
                    throw new NotFoundException("Book", item.BookId);

                if (!book.IsAvailable)
                    return Result.Failure($"Book '{book.Title}' is not available.");

                var orderItem = new OrderItem
                {
                    BookId = book.Id,
                    Quantity = item.Quantity,
                    UnitPrice = book.Price
                };

                totalAmount += book.Price * item.Quantity;
                orderItems.Add(orderItem);
            }

            var order = new Domain.Entities.Order
            {
                UserId = request.UserId,
                TotalAmount = totalAmount,
                ShippingAddress = request.ShippingAddress,
                OrderItems = orderItems
            };

            await _uow.OrderRepository.AddAsync(order, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success("Order placed successfully!");
        }
    }
}
