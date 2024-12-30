using TrackingOrderSystem.Data.Entities;

namespace TrackingOrderSystem.Data.Repositories.Interface
{
    public interface IOrderItemRepositories
    {
        Task<IEnumerable<OrderItem>> GetAllOrderItemAsync();
        Task<OrderItem> GetOrderItemByIdAsync(int id);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(OrderItem orderItem, int id);
        Task DeleteOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemByOrderIdAsync(int orderId);
    }
}
