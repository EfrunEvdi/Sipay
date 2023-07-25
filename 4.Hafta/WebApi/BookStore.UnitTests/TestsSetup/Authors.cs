using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.TestsSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
            new Author
            {
                Name = "Eric",
                Surname = "Ries",
                DateOfBirth = new DateTime(1978, 09, 22)
            },
            new Author
            {
                Name = "Charlotte",
                Surname = "Gilman",
                DateOfBirth = new DateTime(1860, 08, 3)
            },
            new Author
            {
                Name = "Frank",
                Surname = "Herbert",
                DateOfBirth = new DateTime(1920, 10, 08)
            });
        }
    }
}
