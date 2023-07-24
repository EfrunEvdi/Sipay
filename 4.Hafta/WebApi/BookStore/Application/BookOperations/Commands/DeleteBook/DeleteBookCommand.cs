using BookStore.DBOperations;
using System;
using System.Linq;

namespace BookStore.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookID { get; set; }
        private readonly BookStoreDbContext _context;

        public DeleteBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.BookID == BookID);

            if (book is null)
            {
                throw new InvalidOperationException("Silenecek kitap bulunamadı.");
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}
