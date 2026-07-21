using Application.Features.Book.Queries.GetByAuthorId;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Queries.GetByAuthorIdBook
{
    public class GetByAuthorIdBookValidator : AbstractValidator<GetByAuthorIdBookQuery>
    {
        public GetByAuthorIdBookValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id greaterthan 0");
        }
    }
}
