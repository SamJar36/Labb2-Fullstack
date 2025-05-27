using Labb2_REST_API.Models;
using Labb2_REST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Labb2_REST_API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is null");
            }
            var createdCustomer = await _repository.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(CreateCustomer), new { id = customer.Id }, createdCustomer);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var success = await _repository.DeleteCustomerAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok(new { Message = "Customer deleted successfully." });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _repository.GetAllCustomersAsync();
            return Ok(customers);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Guid");
            }
            var customer = await _repository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound("There is no customer with that Guid");
            }
            return Ok(new { 
                Message = "Customer found.",
                Customer = customer
            });
        }
        [HttpGet("search-by-email/{email}")]
        public async Task<IActionResult> FindCustomerByEmail(string email)
        {
			if (string.IsNullOrEmpty(email))
			{
				return BadRequest("Email cannot be null or empty.");
			}
            var customer = await _repository.FindCustomerByEmailAsync(email);
			if (customer == null)
			{
				return NotFound("No customer found with that email");
			}
			return Ok(new {
                Message = "Customer email found.",
                Customer = customer
            });
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] Customer customer)
		{
            var updatedCustomer = await _repository.UpdateCustomerAsync(id, customer);
			if (customer == null)
			{
				return NotFound("Customer not found");
			}

			return Ok(new { 
                Message = "Customer updated successfully.", Customer = updatedCustomer });
		}

    }
}
