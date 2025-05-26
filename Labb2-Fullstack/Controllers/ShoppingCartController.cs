using Labb2_REST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    }
}
