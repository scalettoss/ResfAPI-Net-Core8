using Microsoft.CodeAnalysis;
using TrackingOrderSystem.Data.Entities;
using TrackingOrderSystem.Data.Input;
using TrackingOrderSystem.Data.Repositories;
using TrackingOrderSystem.Data.Repositories.Interface;
using TrackingOrderSystem.Exceptios;

namespace TrackingOrderSystem.Services
{
    public class OrderItemService
    {
        private readonly IOrderItemRepositories _orderItemRepository;
        private readonly IProductRepositories _productRepository;

        public OrderItemService(IOrderItemRepositories orderItemRepository, IProductRepositories productRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemAsync()
        {
            return await _orderItemRepository.GetAllOrderItemAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                throw new ExceptionHttp(404,"Không tìm thấy chi tiết hóa đơn.");
            }
            return orderItem;
        }

        public async Task AddOrderItemAsync(OrderItemInput orderItem)
        {
            try
            {
                // async/await Result
                var product = _productRepository.GetProductByIdAsync(orderItem.ProductId);
                if (product == null)
                {
                    throw new ExceptionHttp(401, "Sản phẩm không tồn tại.");
                }
                if (product.Result.StockQuantity < orderItem.Quantity)
                {
                    throw new ExceptionHttp(409, "Số lượng sản phẩm không đủ.");
                }
                product.Result.StockQuantity -= orderItem.Quantity;
                var newOrderItem = new OrderItem
                {
                    OrderId = orderItem.OrderId,
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity,
                    TotalPrice = orderItem.Quantity * product.Result.Price,
                };
                await _orderItemRepository.AddOrderItemAsync(newOrderItem);
            }
            catch (Exception ex)
            {
                throw new ExceptionHttp(404,"Lỗi do không tồn hóa đơn hoặc sản phẩm");
            }

        }
        public async Task UpdateOrderItemAsync(OrderItemInput orderItem, int id)
        {
            var existingOrderItem = await _orderItemRepository.GetOrderItemByIdAsync(id);
            if (existingOrderItem == null)
            {
                throw new ExceptionHttp(404,"Không tồn tại chi tiết hóa đơn.");
            }

            var product = await _productRepository.GetProductByIdAsync(orderItem.ProductId);
            if (product == null)
            {
                throw new ExceptionHttp(404,"Sản phẩm không tồn tại.");
            }
            var quantityDifference = orderItem.Quantity - existingOrderItem.Quantity;
            if (quantityDifference > 0)
            {
                if (product.StockQuantity < quantityDifference)
                {
                    throw new ExceptionHttp(409, "Số lượng sản phẩm trong kho không đủ.");
                }
                product.StockQuantity -= quantityDifference;
            }
            else if (quantityDifference < 0)
            {
                product.StockQuantity += Math.Abs(quantityDifference);
            }
            await _productRepository.UpdateProductAsync(product, orderItem.ProductId);
            existingOrderItem.OrderId = orderItem.OrderId;
            existingOrderItem.ProductId = orderItem.ProductId;
            existingOrderItem.Quantity = orderItem.Quantity;
            existingOrderItem.TotalPrice = orderItem.Quantity * product.Price;
            await _orderItemRepository.UpdateOrderItemAsync(existingOrderItem, id);
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var existingOrderItem = await _orderItemRepository.GetOrderItemByIdAsync(id);
            if (existingOrderItem == null)
            {
                throw new ArgumentException("Không tồn tại chi tiết hóa đơn");
            }
            await _orderItemRepository.DeleteOrderItemAsync(existingOrderItem);
        }
        public async Task DeleteOrderItemByOrderIdAsync(int orderId)
        {
            await _orderItemRepository.DeleteOrderItemByOrderIdAsync(orderId);
        }
    }
}
