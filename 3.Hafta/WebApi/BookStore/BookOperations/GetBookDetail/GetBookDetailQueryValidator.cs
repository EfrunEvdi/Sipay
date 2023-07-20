using BookStore.BookOperations.DeleteBook;
using FluentValidation;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(c => c.BookID).NotEmpty().GreaterThan(0);
        }
    }
}
