using Restful.WebApi.DAL.Entities;

namespace Restful.WebApi.FakeService.Abstract
{
    public interface IProductService
    {
        List<Product> ProductList();
        Product GetProductByID(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        bool IsLogin();
    }
}
