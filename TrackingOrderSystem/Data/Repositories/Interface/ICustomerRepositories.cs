using TrackingOrderSystem.Data.Entities;
using TrackingOrderSystem.Data.Input;

namespace TrackingOrderSystem.Data.Repositories.Interface
{
    public interface ICustomerRepositories
    {
        Task<IEnumerable<Customer>> GetAllCustomerAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer, int id);
        Task DeleteCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<Customer> GetCustomerByPhoneAsync(string phone);
    }
}
