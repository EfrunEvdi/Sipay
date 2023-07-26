using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.DBOperations;
using BookStore.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using static BookStore.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(1, "Lord Of The Rings", 1)]
        [InlineData(1, "Lord", 0)]
        [InlineData(0, "Lord", 1)]
        [InlineData(1, "Lord Of", -1)]
        [InlineData(0, "Lor", 0)]
        [InlineData(1, "", 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int genreId)
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookID = bookId;
            command.Model = new UpdateBookModel
            {
                Title = title,
                GenreID = genreId
            };

            // Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldReturnError()
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookID = 1;
            command.Model = new UpdateBookModel
            {
                Title = "Lord Of The Rings",
                GenreID = 1
            };

            // Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookID = 1;
            command.Model = new UpdateBookModel
            {
                Title = "Lord Of The Rings",
                GenreID = 1
            };

            // Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}