using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.DBOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using System.Linq;
using static BookStore.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using Xunit;
using AutoMapper;
using BookStore.Entities;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenBookIDDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var command = new UpdateBookCommand(_context);
            command.BookID = -1;

            // Act - Assert
            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek kitap bulunamadı.");
        }

        [Fact]
        public void WhenGivenBookIDExistsInDb_Book_ShouldBeUpdated()
        {
            // Arrange
            var bookInDB = new Book { Title = "WhenGivenBookIDExistsInDb_Book_ShouldBeUpdated", PageCount = 100, PublishDate = new DateTime(1990, 02, 02), GenreID = 1 };
            var bookCompared = new Book
            {
                Title = bookInDB.Title,
                PageCount = bookInDB.PageCount,
                PublishDate = bookInDB.PublishDate,
                GenreID = bookInDB.GenreID
            };
            _context.Books.Add(bookInDB);
            _context.SaveChanges();

            var command = new UpdateBookCommand(_context);
            command.BookID = bookInDB.BookID;
            command.Model = new UpdateBookModel { Title = "UpdatedTitle", GenreID = 2 };

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var bookUpdated = _context.Books.SingleOrDefault(b => b.BookID == bookInDB.BookID);
            bookUpdated.Should().NotBeNull();
            bookUpdated.PageCount.Should().NotBe(bookCompared.PageCount);
            bookUpdated.PublishDate.Should().NotBe(bookCompared.PublishDate);
            bookUpdated.GenreID.Should().NotBe(bookCompared.GenreID);
        }
    }
}