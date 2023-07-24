using FluentValidation;
using System;

namespace BookStore.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Model.Title).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Model.GenreID).GreaterThan(0).LessThan(5);
            RuleFor(x => x.Model.PageCount).GreaterThan(0);
            RuleFor(x => x.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}
