using Labb2_REST_API.Models;
using Labb2_REST_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Labb2_REST_API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
		private readonly IProductRepository _repository;
		public ProductsController(IProductRepository repository)
		{
			_repository = repository;
		}
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Customer data is null");
            }
            var createdProduct = await _repository.CreateProductAsync(product);
            return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, createdProduct);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _repository.GetProductByIdAsync(id);
			if (product == null)
			{
				return NotFound("Product not found");
			}

			await _repository.DeleteProductAsync(product);

			return Ok(new { 
				Message = "Product deleted successfully.",
				Product = product
			});
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
		{
			if (id != updatedProduct.Id)
			{
				return BadRequest("Product ID mismatch");
			}
			var product = await _repository.GetProductByIdAsync(id);
			if (product == null)
			{
				return NotFound("Product not found");
			}

			product.ProductName = updatedProduct.ProductName;
			product.ProductDescription = updatedProduct.ProductDescription;
			product.Price = updatedProduct.Price;
			product.ProductCategory = updatedProduct.ProductCategory;
			product.Status = updatedProduct.Status;

			await _repository.UpdateProductAsync(product);

			return Ok(new { 
				Message = "Product updated successfully.",
				Product = product});
		}
		[HttpPatch("{id}/status/{statusUpdate}")]
		public async Task<IActionResult> UpdateStatus(int id, string statusUpdate)
		{
			var product = await _repository.GetProductByIdAsync(id);

			if (product == null)
			{
				return NotFound("Product not found");
			}
			if (statusUpdate != "in stock" && statusUpdate != "out of stock")
			{
				return BadRequest("Only 'in stock' or 'out of stock");
			}

			product.Status = statusUpdate;
			await _repository.UpdateProductAsync(product);
			
			return Ok("Status updated");
		}

		[HttpGet("search-by-name/{name}")]
		public async Task<IActionResult> SearchProductByName(string name)
		{
			var product = await _repository.GetProductByNameAsync(name);

			if (product == null)
			{
				return NotFound("Product not found");
			}

			return Ok(new
			{
				Message = "Product found.",
				Product = product
			});
		}
		[HttpGet("search-by-id/{id}")]
		public async Task<IActionResult> SearchProductById(int id)
		{
			var product = await _repository.GetProductByIdAsync(id);

			if (product == null)
			{
				return NotFound("Product not found");
			}

			return Ok(new {
				Message = "Product found.",
				Product = product
			});
		}
	}
}
