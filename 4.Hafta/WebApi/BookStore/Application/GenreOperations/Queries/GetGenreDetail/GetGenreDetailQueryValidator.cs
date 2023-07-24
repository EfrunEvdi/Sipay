using FluentValidation;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;

namespace GenreStore.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(x => x.GenreID).NotEmpty().GreaterThan(0);
        }
    }
}
