using FluentValidation;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(c => c.BookID).NotEmpty().GreaterThan(0);
            RuleFor(c => c.Model.Title).MinimumLength(3).NotEmpty();
            RuleFor(c => c.Model.GenreID).GreaterThan(0).LessThan(5);
        }
    }
}
