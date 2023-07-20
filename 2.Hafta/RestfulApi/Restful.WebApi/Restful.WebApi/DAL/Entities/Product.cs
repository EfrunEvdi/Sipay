using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace Restful.WebApi.DAL.Entities
{
    public class Product
    {
        public int ID { get; set; }

        [DisplayName("Product Name")]
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name { get; set; }

        [DisplayName("Product Category")]
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string Category { get; set; }

        [DisplayName("Product Barcod Number")]
        [Range(minimum: 100, maximum: 500)]
        [Required]
        public int Barcod { get; set; }


        [DisplayName("Product Price")]
        [Required]
        [Range(minimum: 100, maximum: 50000)]
        public decimal Price { get; set; }

    }
}