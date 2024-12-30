using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrackingOrderSystem.Data.Entities;
using TrackingOrderSystem.Data.Input;
using TrackingOrderSystem.Data.Repositories.Interface;
using TrackingOrderSystem.Exceptios;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrackingOrderSystem.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepositories _customerRepository;
        private readonly IConfiguration _configuration;
        public CustomerService(ICustomerRepositories customer, IConfiguration configuration)
        {
            _customerRepository = customer;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomerAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                throw new ExceptionHttp(404, "Không tồn tại khách hàng.");
            }
            return customer;
        }

        public async Task AddCustomerAsync(CustomerInput customer)
        {
            //Check email existing.
            var email = await _customerRepository.GetCustomerByEmailAsync(customer.Email);
            if (email != null)
            {
                throw new ExceptionHttp(409,"Email đã được sử dụng bởi một khách hàng khác.");
            }

            //Check phone existing.
            var phone = await _customerRepository.GetCustomerByPhoneAsync(customer.Phone);
            if (phone != null)
            {
                throw new ExceptionHttp(409,"Số điện thoại đã được sử dụng bởi một khách hàng khác.");
            }

            var newCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address,
                Phone = customer.Phone,
                Password = customer.Password,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            await _customerRepository.AddCustomerAsync(newCustomer);
        }

        public async Task UpdateCustomerAsync(CustomerInput customer, int id)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
            {
                throw new ExceptionHttp(404, "Không tồn tại khách hàng.");
            }

            if (existingCustomer.Email != customer.Email)
            {
                var email = await _customerRepository.GetCustomerByEmailAsync(customer.Email);
                if (email != null)
                {
                    throw new ExceptionHttp(409,"Email đã được sử dụng bởi một khách hàng khác.");
                }
            }
            if (existingCustomer.Phone != customer.Phone)
            {
                var phone = await _customerRepository.GetCustomerByPhoneAsync(customer.Phone);
                if (phone != null)
                {
                    throw new ExceptionHttp(409, "Số điện thoại đã được sử dụng bởi một khách hàng khác.");
                }
            }
            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Address = customer.Address;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Password = customer.Password;
            // use interceptor
            existingCustomer.UpdatedAt = DateTime.Now;
            await _customerRepository.UpdateCustomerAsync(existingCustomer,id);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
            {
                throw new ExceptionHttp(404,"Không tồn tại khách hàng");
            }
            await _customerRepository.DeleteCustomerAsync(existingCustomer);
        }

        public async Task<string> LoginCustomerAsync(string email, string password)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(email);
            if (customer == null)
            {
                throw new ExceptionHttp(404, "Email không tồn tại.");
            }
            if (customer.Password != password)
            {
                throw new ExceptionHttp(409,"Mật khẩu không chính xác.");
            }

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Email", customer.Email),
            new Claim("CustomerId", customer.Id.ToString())
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: signIn
            );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenValue);
        }
        
    }
}
