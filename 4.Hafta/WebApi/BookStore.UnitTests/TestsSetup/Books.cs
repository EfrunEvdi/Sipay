using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.TestsSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
            new Book()
            {
                BookID = 1,
                Title = "Lean Startup",
                GenreID = 1, // Personal Growth
                PageCount = 200,
                PublishDate = new DateTime(2001, 06, 12),
                AuthorID = 1
            },
            new Book
            {
                BookID = 2,
                Title = "Herland",
                GenreID = 1, // Science Fiction
                PageCount = 250,
                PublishDate = new DateTime(2010, 05, 23),
                AuthorID = 2
            },
            new Book
            {
                BookID = 3,
                Title = "Dune",
                GenreID = 2, // Noval
                PageCount = 540,
                PublishDate = new DateTime(2001, 12, 21),
                AuthorID = 3
            });
        }
    }
}
