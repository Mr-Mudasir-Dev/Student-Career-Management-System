using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Commands.SubmitFeedback
{
    public class SubmitFeedbackValidator : AbstractValidator<SubmitFeedbackCommand>
    {
        public SubmitFeedbackValidator()
        {
            RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid category.");

            RuleFor(x => x.Message)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Message is required.")
                .MinimumLength(10).WithMessage("Message minimum 10 characters.")
                .MaximumLength(1000).WithMessage("Message maximum 1000 characters.");

            RuleFor(x => x.Rating)
                .Cascade(CascadeMode.Stop)
                .InclusiveBetween(1, 5)
                .When(x => x.Rating.HasValue)
                .WithMessage("Rating must be between 1 and 5.");
        }
    }
}
