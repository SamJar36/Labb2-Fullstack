namespace Shared
{
    public enum OrderStatus
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled,
    }
    public class Order
    {
        public int Id { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalPrice => OrderProducts?.Sum(op => op.Price * op.Quantity) ?? 0;
        public OrderStatus Status { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
    public class CreateOrderRequest
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
    public class OrderStatusUpdateDto
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
