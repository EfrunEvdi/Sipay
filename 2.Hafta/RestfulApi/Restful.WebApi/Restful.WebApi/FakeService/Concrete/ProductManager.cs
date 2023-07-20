using Restful.WebApi.DAL.Entities;
using Restful.WebApi.FakeService.Abstract;

namespace Restful.WebApi.FakeService.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly List<Product> _productList;
        private readonly List<User> _userList;

        public ProductManager()
        {
            _productList = Products.ProductList;
            _userList = Users.UserList;
        }

        public List<Product> ProductList()
        {
            return _productList;
        }

        public Product GetProductByID(int id)
        {
            return _productList.FirstOrDefault(p => p.ID == id);
        }

        public void CreateProduct(Product product)
        {
            int newId = _productList.Max(p => p.ID) + 1;
            product.ID = newId;
            _productList.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            Product existingProduct = _productList.FirstOrDefault(p => p.ID == product.ID);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Category = product.Category;
                existingProduct.Barcod = product.Barcod;
                existingProduct.Price = product.Price;
            }
        }

        public void DeleteProduct(int id)
        {
            Product product = _productList.FirstOrDefault(p => p.ID == id);
            if (product != null)
            {
                _productList.Remove(product);
            }
        }

        public bool IsLogin()
        {
            bool result = _userList.Any(u => u.IsLoggedIn);
            return result;
        }
    }
}
