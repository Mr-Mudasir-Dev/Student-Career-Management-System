using Application.Common;
using Application.Features.Order.Queries.DTOs;
using Domain.Enums.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries.GetOrderByStatus
{
    public class GetOrderByStatusQuery : IRequest<Result<List<OrderDto>>>
    {
        public OrderStatus Status { get; set; }
    }
}
