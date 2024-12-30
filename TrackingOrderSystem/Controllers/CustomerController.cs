using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using TrackingOrderSystem.Data.Entities;
using TrackingOrderSystem.Data.Input;
using TrackingOrderSystem.Data.Repositories.Interface;
using TrackingOrderSystem.Responses;
using TrackingOrderSystem.Services;

namespace TrackingOrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult> GetAllCustomers()
        {
            var customerList = await _customerService.GetAllCustomersAsync();
            var successResponse = new SuccessResponse("Lấy danh sách khách hàng thành công.", customerList);
            return Ok(successResponse);
        }

        [HttpGet]
        [Route("getby/{id}")]
        public async Task<ActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            var successResponse = new SuccessResponse("Lấy thông tin khách hàng thành công.", customer);
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddCustomer(CustomerInput customer)
        {
            await _customerService.AddCustomerAsync(customer);
            var successResponse = new SuccessResponse("Khách hàng đã được thêm thành công.", customer);
            return Ok(successResponse);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult> UpdateCustomer(CustomerInput customer, int id)
        {
            await _customerService.UpdateCustomerAsync(customer, id);
            var successResponse = new SuccessResponse("Cập nhật thông tin khách hàng thành công.", customer);
            return Ok(successResponse);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            var successResponse = new SuccessResponse("Xóa thông tin khách hàng thành công.");
            return Ok(successResponse);
        }

        
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> LoginCustomer(LoginInput loginInput)
        {
            var token = await _customerService.LoginCustomerAsync(loginInput.Email, loginInput.Password);
            var successResponse = new SuccessResponse("Đăng nhập thành công.", new
            {
                Token = token,
            });
            return Ok(successResponse);
        }
        [Authorize]
        [HttpGet]
        [Route("testauthor")]
        public async Task<ActionResult> TestAuthor()
        {
            var successResponse = new SuccessResponse("Test Authorize");
            return Ok(successResponse);
        }
    }
}
