using FluentValidation;

namespace BookStore.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.BookID).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Model.Title).MinimumLength(3).NotEmpty();
            RuleFor(x => x.Model.GenreID).GreaterThan(0).LessThan(5);
        }
    }
}
