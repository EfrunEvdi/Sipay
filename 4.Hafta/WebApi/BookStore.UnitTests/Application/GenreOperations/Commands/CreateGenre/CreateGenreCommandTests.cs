using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using static BookStore.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.CreateGenre
{

    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var genre = new Genre { Name = "Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel() { Name = genre.Name };

            // Act - Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kategori zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // Arrange
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            var model = new CreateGenreModel() { Name = "WhenValidInputsAreGiven_Genre_ShouldBeCreated", };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            genre.IsActive.Should().Be(true);
        }
    }
}