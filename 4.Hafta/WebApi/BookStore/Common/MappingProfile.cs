using AutoMapper;
using static BookStore.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using BookStore.Entities;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using static BookStore.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.AuthorOperation.Commands.CreateAuthor;
using BookStore.Application.AuthorOperation.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperation.Queries.GetAuthors;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname)); // Hem kategori hem de yazar include edildi.
            CreateMap<CreateBookModel, Book>(); // Source -> Target
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            // Genre
            CreateMap<Genre, GenresViewModel>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Genre, GenreDetailViewModel>();

            // Author    
            CreateMap<Author, AuthorsViewModel>().ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<Author, AuthorsDetailViewModel>().ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<CreateAuthorModel, Author>();
        }
    }
}
