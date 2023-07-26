using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using GenreStore.Application.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace BookStore.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenGenreIDIsNotGreaterThenZero_Validator_ShouldBeReturnError()
        {
            // Arrange
            GetGenreDetailQuery command = new GetGenreDetailQuery(null, null);
            command.GenreID = 0;

            // Act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenGenreIDIsGreaterThenZero__Validator_ShouldNotReturnError()
        {
            // Arrange
            GetGenreDetailQuery command = new GetGenreDetailQuery(null, null);
            command.GenreID = 1;
            // Act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}