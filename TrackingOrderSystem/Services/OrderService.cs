using Microsoft.EntityFrameworkCore;
using TrackingOrderSystem.Data.Entities;
using TrackingOrderSystem.Data.Input;
using TrackingOrderSystem.Data.Repositories;
using TrackingOrderSystem.Data.Repositories.Interface;
using static TrackingOrderSystem.Responses.ResponseBase;

namespace TrackingOrderSystem.Services
{
    public class OrderService
    {
        private readonly IOrderRepositories _orderRepository;
        private readonly IOrderItemRepositories _orderItemRepositories;

        public OrderService(IOrderRepositories orderRepository, IOrderItemRepositories orderItemRepositories)
        {
            _orderRepository = orderRepository;
            _orderItemRepositories = orderItemRepositories;
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await _orderRepository.GetAllOrderAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new ArgumentException("Không tìm thấy hóa đơn.");
            }
            return order;
        }

        public async Task AddOrderAsync(OrderInput order)
        {
            try
            {
                var newOrder = new Order
                {
                    CustomerId = order.CustomerId,
                    OrderDate = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    TotalAmount = order.TotalAmount,
                    ShippingAddress = order.ShippingAddress,
                    Status = order.Status,
                    PaymentStatus = order.PaymentStatus,
                    UpdatedAt = DateTime.Now,
                };
                await _orderRepository.AddOrderAsync(newOrder);
            } catch (Exception ex)
            {
                throw new ArgumentException("Lỗi do không tồn tại khách hàng");
            }
            
        }

        public async Task UpdateOrderAsync(OrderInput order, int id)
        {
            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);

            if (existingOrder == null)
            {
                throw new ArgumentException("Không tồn tại hóa đơn");
            }
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.OrderDate = DateTime.Now;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.ShippingAddress = order.ShippingAddress;
            existingOrder.Status = order.Status;
            existingOrder.PaymentStatus = order.PaymentStatus;
            existingOrder.UpdatedAt = DateTime.Now;

            await _orderRepository.UpdateOrderAsync(existingOrder, id);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                throw new ArgumentException("Không tồn tại hóa đơn");
            }
            //Delete Order.
            await _orderRepository.DeleteOrderAsync(existingOrder);
            //Delete OrderItem inside Order.
            await _orderItemRepositories.DeleteOrderItemByOrderIdAsync(existingOrder.Id);
        }
    }
}
