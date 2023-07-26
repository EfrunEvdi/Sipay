using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using Xunit;
using static BookStore.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("Gen")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            // Arrange
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel { Name = name };

            // Act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel { Name = "Genre" };

            // Act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}