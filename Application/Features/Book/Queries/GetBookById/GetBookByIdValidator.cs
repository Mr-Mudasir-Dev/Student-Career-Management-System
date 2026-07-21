using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Queries.GetBookById
{
    public class GetBookByIdValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id greaterthan 0");
        }
    }
}
