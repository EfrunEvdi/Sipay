using FluentValidation;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookStore.Application.AuthorOperation.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Model.Surname).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Model.DateOfBirth)
                          .NotEmpty().WithMessage("Date of birth is required.")
                          .Must(date => date <= DateTime.Now.Date.AddYears(-18))
                          .WithMessage("You must be at least 18 years old.");
        }
    }
}