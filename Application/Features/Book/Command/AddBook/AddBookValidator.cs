using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Command.AddBook
{
    public class AddBookValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookValidator()
        {
            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters.")
                .MaximumLength(300).WithMessage("Title maximum 300 characters.");

            RuleFor(x => x.Price)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.PublishedDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Published date is required.");

            RuleFor(x => x.Language)
                .Cascade(CascadeMode.Stop)
                .IsInEnum().WithMessage("Invalid language.");

            RuleFor(x => x.AuthorIds)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("At least one author is required.");

            RuleFor(x => x.GenreIds)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("At least one genre is required.");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(10).WithMessage("Description must be at least 10 characters.")
                .MaximumLength(2000).WithMessage("Description maximum 2000 characters.")
                .When(x => x.Description != null);
        }
    }
}
