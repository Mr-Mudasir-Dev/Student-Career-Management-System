using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetFeedbackById
{
    public class GetFeedbackByIdValidator : AbstractValidator<GetFeedbackByIdQurey>
    {
        public GetFeedbackByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("id is required");
        }
    }
}
