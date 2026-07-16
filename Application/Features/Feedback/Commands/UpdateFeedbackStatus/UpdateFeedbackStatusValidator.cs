using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Commands.UpdateFeedbackStatus
{
    public class UpdateFeedbackStatusValidator : AbstractValidator<UpdateFeedbackStatusCommand>
    {
        public UpdateFeedbackStatusValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Invalid Feedback Id.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status.");
        }
    }
}
