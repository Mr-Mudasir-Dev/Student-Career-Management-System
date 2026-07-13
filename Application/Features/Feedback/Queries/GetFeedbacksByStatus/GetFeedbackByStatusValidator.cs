using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetFeedbacksByStatus
{
    public class GetFeedbackByStatusValidator : AbstractValidator<GetFeedbacksByStatusQuery> 
    {
        public GetFeedbackByStatusValidator()
        {
            RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid feedback status.");
        }
    }
}
