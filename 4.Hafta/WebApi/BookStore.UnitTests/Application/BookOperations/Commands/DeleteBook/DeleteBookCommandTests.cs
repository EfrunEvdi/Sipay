using AutoMapper;
using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.DBOperations;
using BookStore.Entities;
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
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenBookIDDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var command = new DeleteBookCommand(_context);
            command.BookID = -1;

            // Act - Assert
            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı.");
        }

        [Fact]
        public void WhenGivenBookIDExistsInDb_Book_ShouldBeDeleted()
        {
            // Arrange
            var bookInDB = new Book { Title = "WhenGivenBookIDExistsInDb_Book_ShouldBeDeleted", PageCount = 100, PublishDate = new DateTime(1990, 02, 02), GenreID = 1 };
            _context.Books.Add(bookInDB);
            _context.SaveChanges();

            var command = new DeleteBookCommand(_context);
            command.BookID = bookInDB.BookID;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var book = _context.Books.SingleOrDefault(b => b.BookID == bookInDB.BookID);
            book.Should().BeNull();
        }
    }
}
