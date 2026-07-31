using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Review.Queries.GetBookReview
{
    public class GetBookReviewValidator : AbstractValidator<GetBookReviewQuery>
    {
        public GetBookReviewValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("Invalid book id");
        }
    }
}
