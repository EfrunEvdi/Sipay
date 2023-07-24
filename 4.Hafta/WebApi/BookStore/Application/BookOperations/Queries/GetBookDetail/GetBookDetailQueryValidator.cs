using BookStore.Application.BookOperations.Commands.DeleteBook;
using FluentValidation;

namespace BookStore.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(x => x.BookID).NotEmpty().GreaterThan(0);
        }
    }
}
