using System.Reflection.Metadata;
using TrackingOrderSystem.Data.Entities;
using TrackingOrderSystem.Data.Input;
using TrackingOrderSystem.Data.Repositories.Interface;

namespace TrackingOrderSystem.Services
{
    public class ProductService
    {
        private readonly IProductRepositories _productRepository;

        public ProductService(IProductRepositories productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _productRepository.GetAllProductAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new ArgumentException("Không tìm thấy sản phẩm.");
            }
            return product;
        }

        public async Task AddProductAsync(ProductInput product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            await _productRepository.AddProductAsync(newProduct);
        }

        public async Task UpdateProductAsync(ProductInput product, int id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);

            // check customer null
            if (existingProduct == null)
            {
                throw new ArgumentException("Không tồn tại sản phẩm");
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.SKU = product.SKU;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.UpdatedAt = DateTime.Now;
            await _productRepository.UpdateProductAsync(existingProduct, id);
        }

        public async Task DeleteProductAsync(int id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new ArgumentException("Không tồn tại sản phẩm");
            }
            await _productRepository.DeleteProductAsync(existingProduct);
        }
    }
}
