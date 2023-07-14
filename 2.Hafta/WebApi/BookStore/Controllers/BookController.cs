using BookStore.DBOperations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

        //private static List<Book> BookList = new List<Book>()
        //{
        //    new Book{ BookID=1, Title="Lean Startup",GenreID=1 /*Personel Growth*/,PublishDate=new DateTime(2001,06,12),PageCount=200 },
        //    new Book{ BookID=2, Title="Herland",GenreID=2 /*Sience Fivtion*/,PublishDate=new DateTime(2010,05,12),PageCount=250 },
        //    new Book{ BookID=3, Title="Dune",GenreID=3 /*Sience Fivtion*/,PublishDate=new DateTime(2008,05,25),PageCount=300 },
        //};

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.BookID).ToList<Book>();
            return bookList; // endpoint
        }

        [HttpGet("{id}")]
        public Book GetByID(int id)
        {
            var book = _context.Books.Where(book => book.BookID == id).SingleOrDefault();
            return book; // endpoint
        }

        //[HttpGet]
        //public Book Get([FromQuery]int id)
        //{
        //    var book = BookList.Where(x => x.BookID == id).SingleOrDefault();
        //    return book;  
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);

            if (book is not null)
            {
                return BadRequest();

            }

            _context.Books.Add(newBook); //db eklendi save lazım
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] Book updateBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.BookID == id);

            if (book is null)
            {
                return BadRequest();

            }

            book.GenreID = updateBook.GenreID != default ? updateBook.GenreID : book.GenreID;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.GenreID == id);

            if (book is null)
            {
                return BadRequest();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return Ok();
        }


    }
}
