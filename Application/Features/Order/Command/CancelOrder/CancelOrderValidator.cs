using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Command.CancelOrder
{
    public class CancelOrderValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("OrderId greaterthan 0");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");
        }
    }
}
