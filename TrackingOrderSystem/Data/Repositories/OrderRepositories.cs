using Microsoft.EntityFrameworkCore;
using TrackingOrderSystem.Data.Entities;
using TrackingOrderSystem.Data.Repositories.Interface;

namespace TrackingOrderSystem.Data.Repositories
{
    public class OrderRepositories : IOrderRepositories
    {
        private readonly MyDbContext _dbContext;

        public OrderRepositories(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
           return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateOrderAsync(Order order, int id)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
