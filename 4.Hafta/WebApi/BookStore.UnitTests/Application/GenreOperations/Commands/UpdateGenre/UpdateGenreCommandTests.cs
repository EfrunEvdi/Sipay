using AutoMapper;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using static BookStore.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIDDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var command = new UpdateGenreCommand(_context);
            command.GenreID = -1;

            // Act - Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü bulunamadı.");
        }

        [Fact]
        public void WhenGivenNameIsSameWithAnotherGenre_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var genreInDB = new Genre { Name = "Genre" };
            var genreSameInDB = new Genre { Name = "Genre" };

            _context.Genres.Add(genreInDB);
            _context.Genres.Add(genreSameInDB);
            _context.SaveChanges();

            var command = new UpdateGenreCommand(_context);
            command.GenreID = genreInDB.GenreID;
            command.Model = new UpdateGenreModel { Name = genreSameInDB.Name };

            // Act - Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kategori zaten mevcut.");
        }

        [Fact]
        public void WhenGivenGenreIDExistsInDb_Genre_ShouldBeUpdated()
        {
            // Arrange
            var genreInDB = new Genre { Name = "WhenGivenGenreIDExistsInDb_Genre_ShouldBeUpdated" };
            var genreCompared = new Genre { Name = genreInDB.Name };
            _context.Genres.Add(genreInDB);
            _context.SaveChanges();

            var command = new UpdateGenreCommand(_context);
            command.GenreID = genreInDB.GenreID;
            command.Model = new UpdateGenreModel { Name = "UpdatedName", IsActive = false };

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var genreUpdated = _context.Genres.SingleOrDefault(b => b.GenreID == genreInDB.GenreID);
            genreUpdated.Should().NotBeNull();
            genreUpdated.Name.Should().NotBe(genreCompared.Name);
            genreUpdated.Should().NotBe(genreCompared.IsActive);
        }
    }
}