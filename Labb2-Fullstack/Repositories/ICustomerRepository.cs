using Labb2_REST_API.Models;

namespace Labb2_REST_API.Repositories
{
	public interface ICustomerRepository
	{
		Task<Customer> CreateCustomerAsync(Customer customer);
		Task<Customer> GetCustomerByIdAsync(Guid id);
		Task<IEnumerable<Customer>> GetAllCustomersAsync();
		Task<Customer> FindCustomerByEmailAsync(string email);
		Task<Customer> UpdateCustomerAsync(Guid id, Customer customer);
		Task<bool> DeleteCustomerAsync(Guid id);
	}
}
