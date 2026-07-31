using Application.Common;
using Application.Interface;
using Domain.Entities;
using Domain.Enums.Order;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Command.CancelOrder
{
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        public CancelOrderHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepository
                .GetByIdAsync(request.OrderId, cancellationToken);

            if (order == null)
                throw new NotFoundException("Order", request.OrderId);

            if (order.UserId != request.UserId)
                throw new ForbiddenException("You can only Cancel your own Order.");

            if (order.Status != OrderStatus.Pending)
                return Result.Failure("Only pending order can be cancel.");

            order.Status = OrderStatus.Cancelled;
            order.UpdatedAt = DateTime.UtcNow;

            _uow.OrderRepository.UpdateAsync(order);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success("Order cancel successfully!");
        }
    }
}
