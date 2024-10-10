using StockControl.API.Models;

namespace StockControl.API.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProductsAsync();
        public Task<Product> GetProductByIdAsync(int id);
        public Task CreateProductAsync(Product product);
        public Task UpdateProductAsync(Product product);
        public Task DeleteProductAsync(int id);
        public Task SaveProductPartNumberAsync(int id, string partNumber);
    }
}
