using basic_inventory_management_api.Enums;
using basic_inventory_management_api.Models;
using basic_inventory_management_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace basic_inventory_management_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("Get All Product")]
        public ActionResult<IEnumerable<Product>> GetAllProduct()
        {
            var product = _productService.GetAllProduct().OrderBy(p => p.DateAdded);
            return Ok(product);
        }

        [HttpGet("{Id:guid}")]
        public ActionResult<IEnumerable<Product>> GetProductById(Guid Id)
        {
            var product = _productService.GetProductById(Id);
            if (product == null)
            {
                _logger.LogWarning("Product not found");
                return NotFound();
            }
            return Ok(product);

        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Product>> Search(string? name = null, Category? category = null)
        {
            try
            {
                var product = _productService.Search(name, category);

                if (!product.Any())
                {
                    _logger.LogInformation("Product not found");
                    return NotFound();

                }
                _logger.LogInformation("Search Ok");
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error");
                return BadRequest(ex);
            }

        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            try
            {
                _productService.AddProduct(product);
                _logger.LogInformation("Poduct Added");
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Product Added Failed", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id:Guid}")]
        public IActionResult UpdateProduct(Guid Id, Product product)
        {
            var updatedProduct = _productService.GetProductById(Id);
            if (updatedProduct == null)
            {
                _logger.LogWarning($"Product with Id not found");
                return NotFound();
            }
            _logger.LogInformation($"Product {product.Id}");
            product.Id = Id;
            _productService.UpdateProduct(Id, product);
            _logger.LogInformation("Updated Successful");
            return Ok("Product Updated Successful");
        }
        [HttpDelete("{Id:Guid}")]
        public IActionResult DeleteProduct(Guid Id)
        {
            _logger.LogInformation("Delete Product Accepted");

            var deleted = _productService.DeleteProductById(Id);
            if (!deleted)
            {
                _logger.LogInformation("Product by Id not found");
                return BadRequest("Product Not Found");
            }
            _logger.LogInformation("Product Deleted");
            return Ok("Product Deleted");

        }
    }
}
