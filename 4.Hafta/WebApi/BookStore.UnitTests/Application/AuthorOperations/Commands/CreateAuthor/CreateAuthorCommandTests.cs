using AutoMapper;
using BookStore.Application.AuthorOperation.Commands.CreateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System.Linq;
using System;
using Xunit;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var author = new Author
            {
                Name = "WhenAlreadyExistAuthorNameIsGiven",
                Surname = "InvalidOperationException_ShouldBeReturn",
                DateOfBirth = new DateTime(1995, 08, 02)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_mapper, _context);
            command.Model = new CreateAuthorModel() { Name = author.Name, Surname = author.Surname };

            // Act - Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_mapper, _context);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                Name = "WhenValidInputsAreGiven",
                Surname = "Author_ShouldBeCreated",
                DateOfBirth = DateTime.Now.Date.AddYears(-18)
            };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var author = _context.Authors.SingleOrDefault(b => b.Name == model.Name);

            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.DateOfBirth.Should().Be(model.DateOfBirth);
        }
    }
}