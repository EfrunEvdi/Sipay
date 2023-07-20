using Restful.WebApi.DAL.Entities;

namespace Restful.WebApi.Models
{
    public class ProductList
    {
        public List<Product> Products { get; set; }

        public ProductList()
        {
            Products = new List<Product>
            {
               new Product { ID = 1, Category = "Kalem", Name = "Kalem 1", Barcod = 123, Price = 100 },
               new Product { ID = 2, Category = "Kalem", Name = "Kalem 2", Barcod = 124, Price = 200 },
               new Product { ID = 3, Category = "Kalem", Name = "Kalem 3", Barcod = 125, Price = 300 },
               new Product { ID = 4, Category = "Silgi", Name = "Silgi 1", Barcod = 345, Price = 100 },
               new Product { ID = 5, Category = "Silgi", Name = "Silgi 2", Barcod = 346, Price = 200 },
               new Product { ID = 6, Category = "Silgi", Name = "Silgi 3", Barcod = 347, Price = 300 }

            };
        }
    }
}