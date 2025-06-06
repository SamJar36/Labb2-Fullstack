﻿using Labb2_REST_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb2_REST_API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FullstackContext _context;
        public OrderRepository(FullstackContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .ToListAsync();
        }
        public async Task<Shared.Order> AddAsync(Shared.Order sharedOrder)
        {
            // Map from Shared.Order to EF Order
            var efOrder = new Labb2_REST_API.Models.Order
            {
                CustomerId = sharedOrder.CustomerId,
                OrderDate = sharedOrder.OrderDate,
                Status = (Labb2_REST_API.Models.OrderStatus)sharedOrder.Status,
                OrderProducts = sharedOrder.OrderProducts.Select(p => new Labb2_REST_API.Models.OrderProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    Price = p.Price
                }).ToList()
            };

            _context.Orders.Add(efOrder);
            await _context.SaveChangesAsync();
            sharedOrder.Id = efOrder.Id;
            return sharedOrder;
        }
        public async Task<bool> DeleteAsync(int orderId)
        {
            var order = await _context.Orders
                            .Include(o => o.OrderProducts)
                            .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order != null)
            {
                _context.OrderProducts.RemoveRange(order.OrderProducts);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }
        public async Task UpdateStatusAsync(int orderId, Shared.OrderStatus newStatus)
        {
            var existingOrder = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (existingOrder != null)
            {
                existingOrder.Status = (Models.OrderStatus)newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
