using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingOrderSystem.Data.Input;
using TrackingOrderSystem.Responses;
using TrackingOrderSystem.Services;

namespace TrackingOrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult> GetAllOrder()
        {
            var orderList = await _orderService.GetAllOrderAsync();
            var successResponse = new SuccessResponse("Lấy danh sách hóa đơn thành công.", orderList);
            return Ok(successResponse);
        }

        [HttpGet]
        [Route("getby/{id}")]
        public async Task<ActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            var successResponse = new SuccessResponse("Lấy thông tin hóa đơn thành công.", order);
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddOrder(OrderInput order)
        {
            await _orderService.AddOrderAsync(order);
            var successResponse = new SuccessResponse("Hóa đơn đã được thêm thành công.", order);
            return Ok(successResponse);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult> UpdateOrder(OrderInput order, int id)
        {
            await _orderService.UpdateOrderAsync(order, id);
            var successResponse = new SuccessResponse("Cập nhật thông tin hóa đơn thành công.", order);
            return Ok(successResponse);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            var successResponse = new SuccessResponse("Xóa thông tin hóa đơn thành công.");
            return Ok(successResponse);
        }
    }
}
