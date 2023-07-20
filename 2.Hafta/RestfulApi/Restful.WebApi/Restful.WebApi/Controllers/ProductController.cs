using Microsoft.AspNetCore.Mvc;
using Restful.WebApi.DAL.Entities;
using Restful.WebApi.Exceptions;
using Restful.WebApi.FakeService.Abstract;
using UnauthorizedAccessException = Restful.WebApi.Exceptions.UnauthorizedAccessException;


namespace Restful.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger, IUserService userService)
        {
            _productService = productService;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            _logger.LogInformation("Entered ProductList action");
            if (_productService.IsLogin() == true)
            {
                if (_productService.ProductList().Count == 0)
                {
                    throw new NotFoundException("Product not found."); // 404 Not Found yanıtı
                }

                _logger.LogInformation("Exited GetAllProducts action");
                return Ok(_productService.ProductList()); // Ürünleri döndürme
            }

            else
            {
                _logger.LogInformation("Exited GetAllProducts action");
                throw new UnauthorizedAccessException("Please try again after logging in.");
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetProductByID(int id)
        {
            _logger.LogInformation($"Entered GetProductByID action for ID: {id}");
            if (_productService.IsLogin() == true)
            {
                var productValue = _productService.GetProductByID(id); // Ürünü arama

                if (productValue == null)
                {
                    throw new NotFoundException("Product not found."); // 404 Not Found yanıtı
                }
                _logger.LogInformation($"Exited GetProductByID action for ID: {id}");
                return Ok(productValue); // Ürünü döndürme
            }

            else
            {
                _logger.LogInformation("Exited GetAllProducts action");
                throw new UnauthorizedAccessException("Please try again after logging in.");
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            _logger.LogInformation("Entered CreateProduct action");
            if (_productService.IsLogin() == true)
            {
                product.ID = Products.ProductList.Count() + 1;

                _productService.CreateProduct(product);

                _logger.LogInformation("Exited CreateProduct action");
                return Created("", product); // 201 Created yanıtı
            }

            else
            {
                _logger.LogInformation("Exited CreateProduct action");
                throw new UnauthorizedAccessException("Please try again after logging in.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            _logger.LogInformation($"Entered UpdateProduct action for ID: {id}");
            if (_productService.IsLogin() == true)
            {
                var productValue = Products.ProductList.FirstOrDefault(x => x.ID == id);

                if (productValue == null)
                {
                    throw new NotFoundException("Product not found."); // 404 Not Found yanıtı
                }

                _productService.UpdateProduct(product);

                _logger.LogInformation($"Exited UpdateProduct action for ID: {id}");
                return Ok(Products.ProductList); // Güncellenmiş ve diğer ürünleri döndürme
            }

            else
            {
                _logger.LogInformation($"Exited UpdateProduct action for ID: {id}");
                throw new UnauthorizedAccessException("Please try again after logging in.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _logger.LogInformation($"Entered DeleteProduct action for ID: {id}");
            if (_productService.IsLogin() == true)
            {
                var productValue = Products.ProductList.FirstOrDefault(x => x.ID == id);

                if (productValue == null)
                {
                    throw new NotFoundException("Product not found."); // 404 Not Found yanıtı
                }

                _productService.DeleteProduct(id);

                _logger.LogInformation($"Exited DeleteProduct action for ID: {id}");
                return Ok(Products.ProductList); // Silinmiş ürünsüz listeyi döndürme
            }

            else
            {
                _logger.LogInformation($"Exited DeleteProduct action for ID: {id}");
                throw new UnauthorizedAccessException("Please try again after logging in.");
            }
        }

        // ------------------------------------------------------------------------------------------

        //[HttpPatch("{id}")] // Ürünü Kısmi Güncelleme (Patch)
        //public IActionResult PatchProduct(int id, [FromBody] Product product)
        //{
        //    var productValue = Products.ProductList.FirstOrDefault(x => x.ID == id);

        //    if (productValue == null)
        //    {
        //        return NotFound(); // 404 Not Found yanıtı
        //    }

        //    productValue.Name = product.Name ?? productValue.Name;
        //    productValue.Category = product.Category ?? productValue.Category;

        //    return Ok(Products.ProductList); // Kısmi güncellenmiş ve diğer ürünleri döndürme
        //}

        //[HttpGet("list")]
        //public IActionResult GetProductsByName(string name)
        //{
        //    var filteredProduct = Products.ProductList.Where(p => p.Name.Contains(name)).ToList(); // Burada yazılan harfler her hangi bir üründe geçiyorsa listelenir.
        //    return Ok(filteredProduct);
        //}

        //[HttpGet("sort")]
        //public IActionResult SortProducts(string sortBy, string sortDirection)
        //{
        //    IQueryable<Product> sortedProducts;

        //    switch (sortBy)
        //    {
        //        case "name":
        //            sortedProducts = sortDirection == "desc" ? Products.ProductList.OrderByDescending(p => p.Name).AsQueryable() : Products.ProductList.OrderBy(p => p.Name).AsQueryable();
        //            break; // İsme göre ister A->Z ister Z->A ya sıralama sorgusu.
        //        case "price":
        //            sortedProducts = sortDirection == "desc" ? Products.ProductList.OrderByDescending(p => p.Price).AsQueryable() : Products.ProductList.OrderBy(p => p.Price).AsQueryable();
        //            break; // Fiyata göre sıralama
        //        default:
        //            sortedProducts = Products.ProductList.AsQueryable();
        //            break;
        //    }

        //    return Ok(sortedProducts.ToList()); // Liste olarak döndürme
        //}
    }
}