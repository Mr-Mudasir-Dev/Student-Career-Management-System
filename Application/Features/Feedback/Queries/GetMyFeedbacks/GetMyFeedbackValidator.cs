using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetMyFeedbacks
{
    public class GetMyFeedbackValidator : AbstractValidator<GetMyFeedbacksQuery>
    {
        public GetMyFeedbackValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");
        }
    }
}
