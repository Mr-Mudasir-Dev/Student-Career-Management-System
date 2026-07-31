using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Review.Command.AddReview
{
    public class AddReviewValidator : AbstractValidator<AddReviewCommand>
    {
        public AddReviewValidator()
        {
            RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("Invalid Book Id.");

            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Invalid Order Id.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment is required.")
                .MaximumLength(1000).WithMessage("Maximum 1000 characters.");
        }
    }
}
