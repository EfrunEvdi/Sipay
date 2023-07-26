using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using System;
using Xunit;

namespace BookStore.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIDDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var genre = new Genre { Name = "WhenGivenGenreIDDoesNotExistInDb_InvalidOperationException_ShouldBeReturn" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);
            command.GenreID = genre.GenreID;

            // act - assert
            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kategori bulunamadı.");
        }

        [Fact]
        public void WhenGivenGenreIdDoesExistInDb_Genre_ShouldBeReturned()
        {
            // Arrange
            var genre = new Genre { Name = "WhenGivenGenreIdDoesExistInDb_Genre_ShouldBeRetuned" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);
            command.GenreID = genre.GenreID;

            // Act
            var genreReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            genreReturned.Should().NotBeNull();
            genreReturned.GenreID.Should().Be(genre.GenreID);
            genreReturned.Name.Should().Be(genre.Name);
        }
    }
}