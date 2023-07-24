using FluentValidation;

namespace BookStore.Application.AuthorOperation.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(x => x.AuthorID).GreaterThan(0);
        }
    }
}
