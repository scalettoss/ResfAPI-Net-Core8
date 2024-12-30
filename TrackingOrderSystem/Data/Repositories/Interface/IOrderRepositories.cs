using TrackingOrderSystem.Data.Entities;

namespace TrackingOrderSystem.Data.Repositories.Interface
{
    public interface IOrderRepositories
    {
        Task<IEnumerable<Order>> GetAllOrderAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order, int id);
        Task DeleteOrderAsync(Order order);
    }
}
