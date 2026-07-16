using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetFeedbackByCategory
{
    public class GetFeedbackByCategoryValidator : AbstractValidator<GetFeedbackByCategoryQuery>
    {
        public GetFeedbackByCategoryValidator()
        {
            RuleFor(x => x.Category)
                .IsInEnum()
                .WithMessage("Invalid category status.");
        }
    }
}
