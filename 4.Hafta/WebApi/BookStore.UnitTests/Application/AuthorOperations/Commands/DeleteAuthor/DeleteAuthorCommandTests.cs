using AutoMapper;
using BookStore.Application.AuthorOperation.Commands.DeleteAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenAuthorIDDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var command = new DeleteAuthorCommand(_context);
            command.AuthorID = -1;

            // Act - Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek yazar bulunamadı.");
        }

        [Fact]
        public void WhenGivenAuthorIDExistsInDb_Author_ShouldBeDeleted()
        {
            // Arrange
            var authorInDB = new Author
            {
                Name = "WhenGivenAuthorIDExistsInDb",
                Surname = "Author_ShouldBeDeleted",
                DateOfBirth = new DateTime(1990, 02, 02)
            };
            _context.Authors.Add(authorInDB);
            _context.SaveChanges();

            var command = new DeleteAuthorCommand(_context);
            command.AuthorID = authorInDB.AuthorID;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var author = _context.Authors.SingleOrDefault(x => x.AuthorID == authorInDB.AuthorID);
            author.Should().BeNull();
        }
    }
}