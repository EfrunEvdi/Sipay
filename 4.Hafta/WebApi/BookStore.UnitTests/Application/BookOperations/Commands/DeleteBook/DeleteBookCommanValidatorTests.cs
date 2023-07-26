using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.DBOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenBookIDIsNotGreaterThenZero_Validator_ShouldReturnError()
        {
            // Arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookID = 0;

            // Act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenBookIDIsGreaterThenZero_Validator_ShouldNotReturnError()
        {
            // Arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookID = 1;

            // Act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}