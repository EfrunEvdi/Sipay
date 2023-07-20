using BookStore.DBOperations;
using System.Linq;
using System;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookID { get; set; }
        public UpdateBookModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public UpdateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.BookID == BookID);

            if (book is null)
            {
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı.");
            }
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreID = Model.GenreID != default ? Model.GenreID : book.GenreID;
            _context.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreID { get; set; }
        }
    }
}
