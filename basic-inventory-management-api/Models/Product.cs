using basic_inventory_management_api.Enums;
using System.ComponentModel;

namespace basic_inventory_management_api.Models
{
    public class Product
    {
       

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Descripton { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

        public Category category { get; set; }
        public DateTime DateAdded { get; set; }

        public Product(string name, string descripton, decimal price, int quantityInStock, Category category)
        {
            Id = Guid.NewGuid();
            Name = name;
            Descripton = descripton;
            Price = price;
            QuantityInStock = quantityInStock;
            this.category = category;
            DateAdded = DateTime.Now;

        }

    }
}
