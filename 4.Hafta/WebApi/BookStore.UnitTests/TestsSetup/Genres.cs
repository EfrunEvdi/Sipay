using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.TestsSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
            new Genre()
            {
                Name = "Personel Growth",
                IsActive = true,
            },
            new Genre()
            {
                Name = "Science Finction",
                IsActive = true,
            },
            new Genre()
            {
                Name = "Noval",
                IsActive = true,
            });
        }
    }
}
