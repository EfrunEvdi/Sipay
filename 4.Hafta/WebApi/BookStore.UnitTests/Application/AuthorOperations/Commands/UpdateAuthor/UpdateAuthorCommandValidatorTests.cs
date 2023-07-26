using BookStore.Application.AuthorOperation.Commands.UpdateAuthor;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using Xunit;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(" ", "S")]
        [InlineData("N", "")]
        [InlineData("N", " ")]
        [InlineData("N", "S")]
        [InlineData("N", "Su")]
        [InlineData("Na", "")]
        [InlineData("Na", " ")]
        [InlineData("Na", "S")]
        [InlineData("Na", "Su")]
        [InlineData("Nam", "S")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel
            {
                Name = name,
                Surname = surname,
                DateOfBirth = new DateTime(1990, 01, 01)
            };

            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenAuthorIsUnder18_Validator_ShouldReturnError()
        {
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel
            {
                Name = "Name",
                Surname = "Surname",
                DateOfBirth = DateTime.Now.AddYears(-18).AddDays(1)
            };

            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorID = 1;
            command.Model = new UpdateAuthorModel
            {
                Name = "Name",
                Surname = "Surname",
                DateOfBirth = new DateTime(1990, 01, 01)
            };
            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}