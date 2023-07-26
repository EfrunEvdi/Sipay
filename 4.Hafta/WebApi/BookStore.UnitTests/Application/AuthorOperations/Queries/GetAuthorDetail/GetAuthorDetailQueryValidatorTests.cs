using BookStore.Application.AuthorOperation.Queries.GetAuthorDetail;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using Xunit;

namespace BookStore.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenAuthorIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
        {
            // Arrange
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(null, null);
            command.AuthorID = 0;

            // Act
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenAuthorIdIsGreaterThenZero__Validator_ShouldNotReturnError()
        {
            // Arrange
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(null, null);
            command.AuthorID = 1;

            // Act
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}