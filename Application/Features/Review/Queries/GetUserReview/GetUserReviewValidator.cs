using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Review.Queries.GetUserReview
{
    public class GetUserReviewValidator : AbstractValidator<GetUserReviewQuery>
    {
        public GetUserReviewValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");
        }
    }
}
