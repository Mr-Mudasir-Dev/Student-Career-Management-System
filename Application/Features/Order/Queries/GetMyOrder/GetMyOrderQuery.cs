using Application.Common;
using Application.Features.Order.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries.GetMyOrder
{
    public class GetMyOrderQuery : IRequest<Result<List<OrderDto>>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
