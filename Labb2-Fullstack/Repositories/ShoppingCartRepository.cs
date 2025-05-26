using Labb2_REST_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb2_REST_API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly FullstackContext _context;

        public ShoppingCartRepository(FullstackContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetAllShoppingCartItemsAsync(Guid id)
        {
            return await _context.ShoppingCartItems
                .Where(item => item.CustomerId == id)
                .Include(item => item.Product)
                .OrderBy(item => item.Product.ProductName)
                .ToListAsync();
        }
    }
}
