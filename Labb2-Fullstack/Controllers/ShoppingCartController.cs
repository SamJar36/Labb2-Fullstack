using Labb2_REST_API.Models;
using Labb2_REST_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Labb2_REST_API.Controllers
{
    [ApiController]
    [Route("api/shoppingcart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _repository;
        public ShoppingCartController(IShoppingCartRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var shoppingCartItems = await _repository.GetAllShoppingCartItemsAsync(id);
            return Ok(shoppingCartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart([FromBody] AddToCartRequest request)
        {
            if (request.CustomerId == Guid.Empty)
                return BadRequest("CustomerId is required.");

            if (request.ProductId <= 0)
                return NotFound("Product not found.");

            var addedItem = await _repository.AddShoppingCartItemAsync(request.CustomerId, request.ProductId);
            return Ok(addedItem);
        }

        [HttpDelete("{customerId}/{productId}")]
        public async Task<IActionResult> DeleteCustomer(Guid customerId, int productId)
        {
            var success = await _repository.RemoveShoppingCartItemAsync(customerId, productId);
            if (!success)
            {
                return NotFound();
            }
            return Ok(new { Message = "Customer deleted successfully." });
        }
    }
}
