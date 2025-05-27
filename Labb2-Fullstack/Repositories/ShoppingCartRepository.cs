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

        public async Task<ShoppingCartItem> AddShoppingCartItemAsync(Guid customerId, Product product)
        {
            var existingItem = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(item => item.CustomerId == customerId && item.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                var newItem = new ShoppingCartItem
                {
                    CustomerId = customerId,
                    ProductId = product.Id,
                    Quantity = 1
                };
                await _context.ShoppingCartItems.AddAsync(newItem);
            }

            await _context.SaveChangesAsync();

            return await _context.ShoppingCartItems
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.CustomerId == customerId && i.ProductId == product.Id);
        }
    }
}
