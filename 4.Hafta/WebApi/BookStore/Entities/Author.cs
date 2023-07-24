using System.Collections.Generic;
using System;

namespace BookStore.Entities
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Book Book { get; set; }
    }
}
