using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Queries.GetAuthorById
{
    public class GetAuthorByIdValidator : AbstractValidator<GetAuthorByIdQuery>
    {
        public GetAuthorByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required");

        }
    }
}
