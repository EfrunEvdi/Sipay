using BookStore.Application.BookOperations.Commands.DeleteBook;
using FluentValidation;

namespace BookStore.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(x => x.GenreID).NotEmpty().GreaterThan(0);
        }
    }
}
