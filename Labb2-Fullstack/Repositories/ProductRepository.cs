using Labb2_REST_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb2_REST_API.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly FullstackContext _context;
		public ProductRepository(FullstackContext context)
		{
			_context = context;
		}
        public async Task<Product> CreateProductAsync(Product product)
		{
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
		{
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _context.Products.FindAsync(id);
		}

		public async Task<Product> GetProductByNameAsync(string name)
		{
			return await _context.Products
							 .Where(p => p.ProductName == name)
							 .FirstOrDefaultAsync();
		}
		public async Task DeleteProductAsync(Product product)
		{
			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateProductAsync(Product product)
		{
			_context.Products.Update(product);
			await _context.SaveChangesAsync();
		}
	}
}
