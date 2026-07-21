using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Queries.GetGenreById
{
    public class GetGenreByIdValidator : AbstractValidator<GetGenreByIdQuery>
    {
        public GetGenreByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
