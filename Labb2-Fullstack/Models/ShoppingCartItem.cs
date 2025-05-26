namespace Labb2_REST_API.Models
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
}
