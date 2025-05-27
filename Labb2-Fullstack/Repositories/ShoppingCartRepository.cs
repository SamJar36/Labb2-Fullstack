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

        public async Task<ShoppingCartItem> AddShoppingCartItemAsync(Guid customerId, int productId)
        {
            var existingItem = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(item => item.CustomerId == customerId && item.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                var newItem = new ShoppingCartItem
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    Quantity = 1
                };
                await _context.ShoppingCartItems.AddAsync(newItem);
            }

            await _context.SaveChangesAsync();

            return await _context.ShoppingCartItems
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.CustomerId == customerId && i.ProductId == productId);
        }

        public async Task<bool> RemoveShoppingCartItemAsync(Guid customerId, int productId)
        {
            var itemToRemove = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(i => i.CustomerId == customerId && i.ProductId == productId);

            if (itemToRemove == null)
                return false;

            if (itemToRemove.Quantity > 1)
            {
                itemToRemove.Quantity -= 1;
                _context.ShoppingCartItems.Update(itemToRemove);
            }
            else
            {
                _context.ShoppingCartItems.Remove(itemToRemove);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveAllFromShoppingCartAsync(Guid customerId)
        {
            var itemsToRemove = await _context.ShoppingCartItems
                .Where(i => i.CustomerId == customerId)
                .ToListAsync();

            if (!itemsToRemove.Any())
                return false;

            _context.ShoppingCartItems.RemoveRange(itemsToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
