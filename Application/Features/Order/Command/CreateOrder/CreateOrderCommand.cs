using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Command.CreateOrder
{
    public class CreateOrderCommand : IRequest<Result>
    {
        public string UserId { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public List<OrderItemRequest> Items { get; set; } = new();
    }

    public class OrderItemRequest
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
