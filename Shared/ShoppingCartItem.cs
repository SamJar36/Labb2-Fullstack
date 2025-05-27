using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal Price => Product.Price * Quantity;
    }
    public class AddToCartRequest
    {
        public Guid CustomerId { get; set; }
        public int ProductId { get; set; }
    }
}
