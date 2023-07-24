using FluentValidation;

namespace BookStore.Application.AuthorOperation.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(x => x.AuthorID).GreaterThan(0).NotEmpty();
        }
    }
}
