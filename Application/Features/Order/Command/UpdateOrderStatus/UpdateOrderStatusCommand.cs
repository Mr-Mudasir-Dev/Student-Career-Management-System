using Application.Common;
using Domain.Enums.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Command.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest<Result>
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
