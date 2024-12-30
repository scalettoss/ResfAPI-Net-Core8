using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingOrderSystem.Data.Input;
using TrackingOrderSystem.Responses;
using TrackingOrderSystem.Services;

namespace TrackingOrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemService _orderItemService;

        public OrderItemController(OrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        [Route("getall")]

        public async Task<ActionResult> GetAllOrderItem()
        {
            var orderItemList = await _orderItemService.GetAllOrderItemAsync();
            var successResponse = new SuccessResponse("Lấy danh sách chi tiết hóa đơn thành công.", orderItemList);
            return Ok(successResponse);
        }

        [HttpGet]
        [Route("getby/{id}")]
        public async Task<ActionResult> GetlOrderItemById(int id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            var successResponse = new SuccessResponse("Lấy thông tin chi tiết hóa đơn thành công.", orderItem);
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddOrderItem(OrderItemInput orderItem)
        {
            await _orderItemService.AddOrderItemAsync(orderItem);
            var successResponse = new SuccessResponse("Chi tiết hóa đơn đã được thêm thành công.", orderItem);
            return Ok(successResponse);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult> UpdateOrderItem(OrderItemInput orderItem, int id)
        {
            await _orderItemService.UpdateOrderItemAsync(orderItem, id);
            var successResponse = new SuccessResponse("Cập nhật thông tin hóa đơn thành công.", orderItem);
            return Ok(successResponse);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteOrderItem(int id)
        {
            await _orderItemService.DeleteOrderItemAsync(id);
            var successResponse = new SuccessResponse("Xóa thông tin hóa đơn thành công.");
            return Ok(successResponse);
        }
    }
}
