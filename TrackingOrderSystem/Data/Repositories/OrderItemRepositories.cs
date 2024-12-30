using Microsoft.EntityFrameworkCore;
using TrackingOrderSystem.Data.Entities;
using TrackingOrderSystem.Data.Repositories.Interface;

namespace TrackingOrderSystem.Data.Repositories
{
    public class OrderItemRepositories : IOrderItemRepositories
    {
        private readonly MyDbContext _dbContext;

        public OrderItemRepositories(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemAsync()
        {
            return await _dbContext.OrderItems.ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _dbContext.OrderItems.FindAsync(id);
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            await _dbContext.OrderItems.AddAsync(orderItem);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateOrderItemAsync(OrderItem orderItem, int id)
        {
            _dbContext.OrderItems.Update(orderItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(OrderItem orderItem)
        {
            _dbContext.OrderItems.Remove(orderItem);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteOrderItemByOrderIdAsync(int orderId)
        {
            var orderItems = await _dbContext.OrderItems
                                             .Where(o => o.OrderId == orderId)
                                             .ToListAsync();
            _dbContext.OrderItems.RemoveRange(orderItems);
            await _dbContext.SaveChangesAsync();
        }
    }
}
