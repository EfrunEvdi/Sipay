using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using Xunit;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenGenreIDIsNotGreaterThenZero_Validator_ShouldReturnError()
        {
            // Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreID = 0;

            // Act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenGenreIDIsGreaterThenZero_Validator_ShouldNotReturnError()
        {
            // Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreID = 1;

            // Act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}