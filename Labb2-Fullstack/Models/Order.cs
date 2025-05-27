namespace Labb2_REST_API.Models
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
        public decimal TotalPrice => OrderProducts?.Sum(op => op.Price) ?? 0;
        public OrderStatus Status { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
