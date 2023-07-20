using AutoMapper;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.UpdateBook.UpdateBookCommand;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);

            //var bookList = _context.Books.OrderBy(x => x.BookID).ToList<Book>();
            //return bookList; // endpoint
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
                query.BookID = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
            //var book = _context.Books.Where(book => book.BookID == id).SingleOrDefault();
            //return book; // endpoint
        }

        //[HttpGet]
        //public Book Get([FromQuery]int id)
        //{
        //    var book = BookList.Where(x => x.BookID == id).SingleOrDefault();
        //    return book;  
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);

                //ValidationResult result = validator.Validate(command);
                //if (result.IsValid)
                //{
                //    foreach (var item in result.Errors)
                //    {
                //        Console.WriteLine("Özellik : " + item.PropertyName + " - Error Message : " + item.ErrorMessage);
                //    }
                //}
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);

            //if (book is not null)
            //{
            //    return BadRequest();

            //}

            //_context.Books.Add(newBook); //db eklendi save lazım
            //_context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.BookID = id;
                command.Model = updateBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //var book = _context.Books.SingleOrDefault(x => x.BookID == id);

            //if (book is null)
            //{
            //    return BadRequest();

            //}

            //book.GenreID = updateBook.GenreID != default ? updateBook.GenreID : book.GenreID;
            //book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            //book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            //book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            //_context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.BookID = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //var book = _context.Books.SingleOrDefault(x => x.GenreID == id);

            //if (book is null)
            //{
            //    return BadRequest();
            //}

            //_context.Books.Remove(book);
            //_context.SaveChanges();

            return Ok();
        }
    }
}