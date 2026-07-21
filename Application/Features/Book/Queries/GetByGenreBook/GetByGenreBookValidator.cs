using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Queries.GetByGenreBook
{
    public class GetByGenreBookValidator : AbstractValidator<GetByGenreBookQuery>
    {
        public GetByGenreBookValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id greaterthan 0");
        }
    }
}
