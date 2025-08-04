using basic_inventory_management_api.Enums;
using basic_inventory_management_api.Models;

namespace basic_inventory_management_api.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProduct();
        Product GetProductById(Guid Id);
        IEnumerable<Product> Search(string name = null, Category? category = null);
        void AddProduct(Product product);
        void UpdateProduct(Guid id,Product product);
        bool DeleteProductById(Guid id);


    }
}
