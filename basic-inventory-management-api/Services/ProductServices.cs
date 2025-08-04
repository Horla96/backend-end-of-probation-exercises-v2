using basic_inventory_management_api.Enums;
using basic_inventory_management_api.Models;

namespace basic_inventory_management_api.Services
{
    public class ProductServices : IProductService
    {
        private static readonly Dictionary<Guid, Product> _products = new();

        public ProductServices()
        {
            var product1 = new Product("Benz", "2020 Model", 500000, 500, Category.Vehicle);
            var product2 = new Product("Rice", "Jollof Rice", 5000, 50, Category.foods);

            _products[product1.Id] = product1;
            _products[product2.Id] = product2;
        }

        public void AddProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException("Invalid Product");
            }
            if (string.IsNullOrWhiteSpace(product.Name))
            {

                throw new ArgumentNullException("Name is Required");
            }
            if (product.Price < 0)
            {
                throw new ArgumentException("Price sould be greater than Zero");
            }
            if(product.QuantityInStock < 0)
            {
                throw new ArgumentException(("Quantity most be greater thn Zero"));
            }
            _products[product.Id] = product;
        }

        public bool DeleteProductById(Guid id)
        {
             return _products.Remove(id);
        }

        public IEnumerable<Product> GetAllProduct()
        {
           return _products.Values;
        }

        public Product GetProductById(Guid Id)
        {
            _products.TryGetValue(Id, out var product);
            return product;
        }

        public IEnumerable<Product> Search(string? name = null, Category? category = null)
        {
            return _products.Values
                 .Where(p =>
                 (string.IsNullOrEmpty(name) || p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                 (!category.HasValue || p.category == category));
        }

        public void UpdateProduct(Guid id, Product product)
        {
            if(_products.ContainsKey(id))
            {
                product.Id = id;
                _products[id]= product; 
            }
        }
    }
}
