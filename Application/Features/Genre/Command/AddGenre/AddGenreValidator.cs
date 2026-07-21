using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Command.AddGenre
{
    public class AddGenreValidator : AbstractValidator<AddGenreCommand>
    {
        public AddGenreValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Genre name is required.")
            .MaximumLength(128).WithMessage("Name maximum 128 characters.")
            .Matches(@"^[A-Za-z\s]+$").WithMessage("Genre name can only contain letters and spaces.");

            RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description maximum 1000 characters.")
            .When(x => x.Description != null);
        }
    }
}
