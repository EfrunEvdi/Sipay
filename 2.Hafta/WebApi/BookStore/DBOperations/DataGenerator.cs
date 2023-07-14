using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initializer(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                // Look for any book.
                if (context.Books.Any())
                {
                    return;   // Data was already seeded
                }

                context.Books.AddRange(
                new Book()
                {
                    BookID = 1,
                    Title = "Lean Startup",
                    GenreID = 1, // Personal Growth
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)

                },
                new Book
                {
                    BookID = 2,
                    Title = "Herland",
                    GenreID = 2, // science fiction
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                new Book
                {
                    BookID = 3,
                    Title = "Dune",
                    GenreID = 2, // science fiction
                    PageCount = 540,
                    PublishDate = new DateTime(2001, 12, 21)
                });

                context.SaveChanges(); //DB ye yazılması için
            };
        }
    }
}
