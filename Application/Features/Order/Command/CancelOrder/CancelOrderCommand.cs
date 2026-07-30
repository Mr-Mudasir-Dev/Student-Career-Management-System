using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Command.CancelOrder
{
    public class CancelOrderCommand : IRequest<Result>
    {
        public int OrderId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
