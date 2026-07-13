using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Commands.DeleteFeedback
{
    public class DeleteFeedbackValidator : AbstractValidator<DeleteFeedbackCommand>
    {
        public DeleteFeedbackValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid Feedback Id.");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}
