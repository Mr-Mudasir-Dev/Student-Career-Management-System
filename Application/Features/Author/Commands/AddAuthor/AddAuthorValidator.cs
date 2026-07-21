using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Commands.AddAuthor
{
    public class AddAuthorValidator : AbstractValidator<AddAuthorCommand>
    {
        public AddAuthorValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Author name is required.")
            .MaximumLength(128).WithMessage("Name maximum 128 characters.")
            .Matches(@"^[A-Za-z\s]+$").WithMessage("Author name can only contain letters and spaces.");

            RuleFor(x => x.Bio)
            .MaximumLength(1000).WithMessage("Bio maximum 1000 characters.")
            .When(x => x.Bio != null);
        }
    }
}
