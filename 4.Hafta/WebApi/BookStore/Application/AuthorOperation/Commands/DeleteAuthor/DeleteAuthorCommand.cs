using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Application.AuthorOperation.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorID { get; set; }
        private readonly IBookStoreDbContext _context;
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.Include(x => x.Book).SingleOrDefault(x => x.AuthorID == AuthorID);
            if (author is null)
            {
                throw new InvalidOperationException("Silinecek yazar bulunamadı.");
            }

            if (author.Book != null && author.Book.BookID != null)
            {
                throw new InvalidOperationException("Önce bu yazara ait kitabı silmelisiniz.");
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
