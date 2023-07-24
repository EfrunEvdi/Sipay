using BookStore.DBOperations;
using System;
using System.Linq;

namespace BookStore.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreID { get; set; }
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Genres.SingleOrDefault(x => x.GenreID == GenreID);

            if (book is null)
            {
                throw new InvalidOperationException("Silenecek kategori bulunamadı.");
            }

            _context.Genres.Remove(book);
            _context.SaveChanges();
        }
    }
}
