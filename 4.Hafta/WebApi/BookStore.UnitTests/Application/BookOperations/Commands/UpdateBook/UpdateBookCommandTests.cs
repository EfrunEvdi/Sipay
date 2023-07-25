using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.DBOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStore.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using Xunit;
using AutoMapper;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNonExistBookIDIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange 
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookID = 0;

            // Act & Assert 
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek kitap bulunamadı.");
        }

        [Fact]
        public void WhenGivenBookIDinDB_Book_ShouldBeUpdate()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            UpdateBookModel model = new UpdateBookModel() { Title = "WhenGivenBookIDinDB_Book_ShouldBeUpdate", GenreID = 1 };
            command.Model = model;
            command.BookID = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.BookID == command.BookID);
            book.Should().NotBeNull();
        }
    }
}
