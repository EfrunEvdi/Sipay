using AutoMapper;
using BookStore.Application.AuthorOperation.Queries.GetAuthorDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using Xunit;

namespace BookStore.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenAuthorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var author = new Author
            {
                Name = "Efrun",
                Surname = "Evdi",
                DateOfBirth = new DateTime(1990, 02, 02)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            _context.Authors.Remove(author);
            _context.SaveChanges();

            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
            command.AuthorID = author.AuthorID;

            // Act - Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı.");
        }

        [Fact]
        public void WhenGivenAuthorIdDoesExistInDb_Author_ShouldBeRetuned()
        {
            // Arrange
            var author = new Author
            {
                Name = "WhenGivenAuthorIdDoesExistInDb",
                Surname = "Author_ShouldBeRetuned",
                DateOfBirth = new DateTime(1990, 02, 02)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
            command.AuthorID = author.AuthorID;

            // Act
            var authorReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            authorReturned.Should().NotBeNull();
            authorReturned.AuthorID.Should().Be(author.AuthorID);
            authorReturned.Name.Should().Be(author.Name);
            authorReturned.Surname.Should().Be(author.Surname);
            authorReturned.DateOfBirth.Should().Be(author.DateOfBirth);
        }
    }
}