using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public List<Book> Books { get; set; }
    }
}
