using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Command.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserName)
           .Cascade(CascadeMode.Stop)
           .NotEmpty()
               .WithMessage("Username is required.")
           .MinimumLength(3)
               .WithMessage("Username must be at least 3 characters long.")
           .MaximumLength(50)
               .WithMessage("Username cannot exceed 50 characters.")
           .Matches(@"^[a-zA-Z0-9_]+$")
               .WithMessage("Username can only contain letters, numbers, and underscores.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("Email address is required.")
                .EmailAddress()
                    .WithMessage("Please enter a valid email address.")
                .MaximumLength(100)
                    .WithMessage("Email address cannot exceed 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("Phone number is required.")
                .Matches(@"^(03\d{9}|\+923\d{9})$")
                     .WithMessage("Please enter a valid Pakistani phone number.");

            RuleFor(x => x.Age)
                .InclusiveBetween(7, 120)
                    .WithMessage("Age must be between 7 and 120.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("Password is required.")
                .MinimumLength(6)
                    .WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(22)
                    .WithMessage("Password cannot exceed 22 characters.")
                .Matches(@"[A-Z]")
                    .WithMessage("Password must contain at least one uppercase letter.");

        }
    }
}
