using Labb2_REST_API.Models;

namespace Labb2_REST_API.Repositories
{
	public interface IProductRepository
	{
		Task<Product> GetProductByIdAsync(int id);
		Task<Product> GetProductByNameAsync(string name);
		Task DeleteProductAsync(Product product);
		Task UpdateProductAsync(Product product);
	}
}
