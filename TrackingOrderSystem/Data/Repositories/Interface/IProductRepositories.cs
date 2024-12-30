using TrackingOrderSystem.Data.Entities;

namespace TrackingOrderSystem.Data.Repositories.Interface
{
    public interface IProductRepositories
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product, int id);
        Task DeleteProductAsync(Product product);
    }
}
