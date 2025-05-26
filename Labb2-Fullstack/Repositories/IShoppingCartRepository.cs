using Labb2_REST_API.Models;

namespace Labb2_REST_API.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<ShoppingCartItem>> GetAllShoppingCartItemsAsync(Guid id);
        //Task<bool> RemoveShoppingCartItemAsync(Guid customerId, int productId);
        //Task<Product> AddShoppingCartItemAsync(Guid id, Product product);
    }
}
