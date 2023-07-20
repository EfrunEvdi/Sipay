using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
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
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
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
            CreateBookCommand command = new CreateBookCommand(_context);

            try
            {
                command.Model = newBook;
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