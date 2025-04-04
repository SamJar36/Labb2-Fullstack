using Labb2_REST_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Labb2_REST_API.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly FullstackContext _context;
		public CustomerRepository(FullstackContext context)
		{
			_context = context;
		}

		public async Task<Customer> CreateCustomerAsync(Customer customer)
		{
            customer.Id = Guid.NewGuid();
            _context.Customers.Add(customer);
			await _context.SaveChangesAsync();
			return customer;
		}
		public async Task<Customer> GetCustomerByIdAsync(Guid id)
		{
			return await _context.Customers.FindAsync(id);
		}
		public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
		{
			return await _context.Customers.ToListAsync();
		}
        
        public async Task<Customer> FindCustomerByEmailAsync(string email)
		{
			return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
		}
		public async Task<Customer> UpdateCustomerAsync(Guid id, Customer customer)
		{
			var existingCustomer = await _context.Customers.FindAsync(id);
			if (existingCustomer == null)
			{
				return null;
			}
			existingCustomer.FirstName = customer.FirstName;
			existingCustomer.LastName = customer.LastName;
			existingCustomer.Email = customer.Email;
			existingCustomer.PhoneNumber = customer.PhoneNumber;
			existingCustomer.Address = customer.Address;

			await _context.SaveChangesAsync();
			return existingCustomer;

		}
		public async Task<bool> DeleteCustomerAsync(Guid id)
		{
			var customer = await _context.Customers.FindAsync(id);
			if (customer == null)
			{
				return false;
			}
			_context.Customers.Remove(customer);
			await _context.SaveChangesAsync();
			return true;
		}

		// ----------------- Orders --------------------
        public async Task<IEnumerable<Product>> GetAllOrderedProductsAsync(Guid customerId)
        {
            return await _context.Customers
                .Where(c => c.Id == customerId)
                .SelectMany(c => c.Products)
                .OrderBy(p => p.ProductName)
                .ToListAsync();
        }
        public async Task<bool> RemoveOrderAsync(Guid id)
		{
			return false;
		}
        public async Task<Product> AddOrderAsync(Guid customerId, Product product)
		{
            var customer = await _context.Customers
				.Include(c => c.Products)
				.FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            customer.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
