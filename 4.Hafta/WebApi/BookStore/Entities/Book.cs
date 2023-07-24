using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Entities
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? BookID { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

        public int GenreID { get; set; }
        public Genre Genre { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
