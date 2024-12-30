using Microsoft.EntityFrameworkCore;
using TrackingOrderSystem.Data.Entities;
using TrackingOrderSystem.Data.Input;
using TrackingOrderSystem.Data.Repositories.Interface;

namespace TrackingOrderSystem.Data.Repositories
{
    public class CustomerRepositories : ICustomerRepositories
    {
        private readonly MyDbContext _dbContext;

        public CustomerRepositories(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateCustomerAsync(Customer customer, int id)
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Customer> GetCustomerByPhoneAsync(string phone)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Phone == phone);
        }
    }
}
