using Labb2_REST_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Shared;

namespace Labb2_REST_API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _repository;

    public OrderController(IOrderRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _repository.GetAllAsync();
        if (orders == null)
        {
            return NotFound();
        }  
        return Ok(orders);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        if (request == null || request.Items == null || !request.Items.Any())
            return BadRequest("No items to order.");

        Shared.Order newOrder = new Shared.Order
        {
            CustomerId = request.CustomerId,
            OrderDate = DateTime.UtcNow,
            Status = Shared.OrderStatus.Shipped,
            OrderProducts = request.Items.Select(item => new Shared.OrderProduct
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList()
        };

        Shared.Order createdOrder = await _repository.AddAsync(newOrder);
        return Ok(createdOrder);
    }
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> Delete(int orderId)
    {
        var success = await _repository.DeleteAsync(orderId);
        if (!success)
        {
            return NotFound();
        }
        return Ok(new { Message = "Order deleted successfully." });
    }
}

