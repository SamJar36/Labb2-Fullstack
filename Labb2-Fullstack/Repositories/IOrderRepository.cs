using Labb2_REST_API.Models;

namespace Labb2_REST_API.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        //Task<Order?> GetByIdAsync(int orderId);
        //Task<List<Order>> GetByUserIdAsync(int userId);
        Task<Shared.Order> AddAsync(Shared.Order order);
        Task UpdateStatusAsync(int orderId, Shared.OrderStatus newStatus);
        Task<bool> DeleteAsync(int orderId);
    }
}
