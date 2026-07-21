using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Queries.GetBySearchBook
{
    public class GetBySearchBookValidator : AbstractValidator<GetBySearchBookQuery>
    {
        public GetBySearchBookValidator()
        {
            RuleFor(x => x.SearchTerm)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Search term is required.")
                .MinimumLength(1).WithMessage("Minimum 1 character required.")
                .MaximumLength(100).WithMessage("Maximum 100 characters allowed.");
        }
    }
}
