using AutoMapper;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIDDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var command = new DeleteGenreCommand(_context);
            command.GenreID = -1;

            // Act - Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silenecek kategori bulunamadı.");
        }

        [Fact]
        public void WhenGivenGenreIDExistsInDb_Genre_ShouldBeDeleted()
        {
            // Arrange
            var genreInDB = new Genre { Name = "WhenGivenGenreIDExistsInDb_Genre_ShouldBeDeleted" };
            _context.Genres.Add(genreInDB);
            _context.SaveChanges();

            var command = new DeleteGenreCommand(_context);
            command.GenreID = genreInDB.GenreID;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var genre = _context.Genres.SingleOrDefault(b => b.GenreID == genreInDB.GenreID);
            genre.Should().BeNull();
        }
    }
}