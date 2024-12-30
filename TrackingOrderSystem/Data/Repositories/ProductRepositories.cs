using Microsoft.EntityFrameworkCore;
using TrackingOrderSystem.Data.Entities;
using TrackingOrderSystem.Data.Repositories.Interface;

namespace TrackingOrderSystem.Data.Repositories
{
    public class ProductRepositories : IProductRepositories
    {
        private readonly MyDbContext _dbContext;

        public ProductRepositories(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(Product product, int id)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

    }
}
