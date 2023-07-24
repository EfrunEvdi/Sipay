using FluentValidation;
using System;

namespace BookStore.Application.AuthorOperation.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
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
