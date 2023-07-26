using AutoMapper;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
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

namespace BookStore.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenBookIDDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var book = new Book { Title = "WhenGivenBookIDDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new DateTime(1990, 02, 02), GenreID = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            _context.Books.Remove(book);
            _context.SaveChanges();

            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookID = book.BookID;

            // Act - Assert
            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");
        }

        [Fact]
        public void WhenGivenBookIDDoesExistInDb_Book_ShouldBeReturned()
        {
            // Arrange
            var book = new Book { Title = "WhenGivenBookIDDoesExistInDb_Book_ShouldBeRetuned", PageCount = 100, PublishDate = new DateTime(1990, 02, 02), GenreID = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookID = book.BookID;

            // Act
            var bookReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            bookReturned.Should().NotBeNull();
            bookReturned.Should().Be(book.BookID);
            bookReturned.Title.Should().Be(book.Title);
            bookReturned.PageCount.Should().Be(book.PageCount);
        }
    }
}