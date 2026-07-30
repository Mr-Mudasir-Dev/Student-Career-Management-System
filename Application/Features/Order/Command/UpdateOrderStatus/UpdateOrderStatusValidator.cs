using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Command.UpdateOrderStatus
{
    public class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusValidator()
        {
            RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage("Invalid Order Id.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status.");
        }
    }
}
