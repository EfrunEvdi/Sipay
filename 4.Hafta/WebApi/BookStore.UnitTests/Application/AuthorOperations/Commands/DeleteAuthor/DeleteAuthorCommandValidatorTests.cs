using BookStore.Application.AuthorOperation.Commands.DeleteAuthor;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using Xunit;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenAuthorIdIsNotGreaterThenZero_Validator_ShouldReturnError()
        {
            // Arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorID = 0;

            // Act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenAuthorIdIsGreaterThenZero_Validator_ShouldNotReturnError()
        {
            // Arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorID = 1;

            // Act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}