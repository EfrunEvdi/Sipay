using BookStore.Application.AuthorOperation.Commands.CreateAuthor;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using Xunit;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", " ")]
        [InlineData("", "E")]
        [InlineData("", "Ev")]
        [InlineData("  ", "E")]
        [InlineData("  ", "Su")]
        [InlineData("   ", "")]
        [InlineData("   ", "E")]
        [InlineData("   ", "Ev")]
        [InlineData("E", "")]
        [InlineData("E", "  ")]
        [InlineData("E", "E")]
        [InlineData("E", "Ev")]
        [InlineData("Ef", "  ")]
        [InlineData("Ef", "E")]
        [InlineData("Efr", "")]
        [InlineData("Efr", "E")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel
            {
                Name = name,
                Surname = surname,
                DateOfBirth = DateTime.Now.Date.AddYears(-19)
            };

            // Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenAuthorIsUnder18_Validator_ShouldReturnError()
        {
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel
            {
                Name = "WhenAuthorIsUnder18",
                Surname = "Validator_ShouldReturnError",
                DateOfBirth = DateTime.Now.Date.AddYears(-18).AddDays(1)
            };

            // Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel
            {
                Name = "WhenValidInputsAreGiven",
                Surname = "Validator_ShouldNotReturnError",
                DateOfBirth = DateTime.Now.Date.AddYears(-18)
            };

            // Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}