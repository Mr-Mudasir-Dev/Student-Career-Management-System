using Application.Common;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Command.UpdateOrderStatus
{
    public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        public UpdateOrderStatusHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepository
                .GetByIdAsync(request.OrderId, cancellationToken);

            if(order == null)
                throw new NotFoundException("Order", request.OrderId);

            order.Status = request.Status;
            order.UpdatedAt = DateTime.UtcNow;

            _uow.OrderRepository.UpdateAsync(order);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success("Order status updated successfully!");
        }
    }
}
