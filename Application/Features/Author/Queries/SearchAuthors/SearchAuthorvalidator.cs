using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Queries.SearchAuthors
{
    public class SearchAuthorvalidator : AbstractValidator<SearchAuthorsQuery>
    {
        public SearchAuthorvalidator()
        {
            RuleFor(x => x.SearchTerm)
            .NotEmpty().WithMessage("Search term is required.")
            .MinimumLength(1).WithMessage("Minimum 1 character required.")
            .MaximumLength(100).WithMessage("Maximum 100 characters allowed.");
        }
    }
}
