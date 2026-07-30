using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries.GetOrderByStatus
{
    public class GetOrderByStatusValidator : AbstractValidator<GetOrderByStatusQuery>
    {
        public GetOrderByStatusValidator()
        {
            RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid feedback status.");
        }
    }
}
