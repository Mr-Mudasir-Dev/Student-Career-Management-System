using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Queries.GetGenreBySeach
{
    public class GetGenreBySearchValidator : AbstractValidator<GetGenreBySearchQuery>
    {
        public GetGenreBySearchValidator()
        {
            RuleFor(x => x.Search)
                .NotEmpty().WithMessage("Search term is required")
                .MinimumLength(1).WithMessage("Minimum 1 character required.")
                .MaximumLength(100).WithMessage("Maximum 100 characters allowed.");
        }
    }
}
