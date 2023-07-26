using AutoMapper;
using BookStore.Application.AuthorOperation.Commands.UpdateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenAuthorIDDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var command = new UpdateAuthorCommand(_context);
            command.AuthorID = -1;

            // Act - Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı.");
        }

        [Fact]
        public void WhenGivenFullNameIsSameWithAnotherAuthor_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange

            var authorInDB = new Author
            {
                Name = "Efrun",
                Surname = "Evdi",
                DateOfBirth = new DateTime(1998, 08, 02)
            };

            var authorSameInDB = new Author
            {
                Name = "Sena",
                Surname = "Celep",
                DateOfBirth = new DateTime(1985, 04, 24)
            };

            _context.Authors.Add(authorInDB);
            _context.Authors.Add(authorSameInDB);
            _context.SaveChanges();

            var command = new UpdateAuthorCommand(_context);
            command.AuthorID = authorInDB.AuthorID;
            command.Model = new UpdateAuthorModel
            {
                Name = authorSameInDB.Name,
                Surname = authorSameInDB.Surname,
                DateOfBirth = new DateTime(1991, 09, 16)
            };

            // Act - Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcuttur.");
        }

        [Fact]
        public void WhenGivenAuthorIDExistsInDb_Author_ShouldBeUpdated()
        {
            // Arrange
            var authorInDB = new Author
            {
                Name = "Name",
                Surname = "Surname",
                DateOfBirth = new DateTime(1998, 08, 02)
            };

            var authorCompared = new Author
            {
                Name = authorInDB.Name,
                Surname = authorInDB.Surname,
                DateOfBirth = authorInDB.DateOfBirth
            };
            _context.Authors.Add(authorInDB);
            _context.SaveChanges();

            var command = new UpdateAuthorCommand(_context);
            command.AuthorID = authorInDB.AuthorID;
            command.Model = new UpdateAuthorModel { Name = "Efrun", Surname = "Evdi", DateOfBirth = new DateTime(1993, 02, 21) };

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var authorUpdated = _context.Authors.SingleOrDefault(x => x.AuthorID == authorInDB.AuthorID);
            authorUpdated.Should().NotBeNull();
            authorUpdated.Name.Should().NotBe(authorCompared.Name);
            authorUpdated.Surname.Should().NotBe(authorCompared.Surname);
            authorUpdated.DateOfBirth.Should().NotBe(authorCompared.DateOfBirth);
        }
    }
}