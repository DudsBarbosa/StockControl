using StockControl.API.Models;
using StockControl.API.Repositories;

namespace StockControl.API.Services
{
    public class ProductService(IProductRepository repository)
    {
        private readonly IProductRepository _repository = repository;

        // Get all products
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _repository.GetProductsAsync();
        }

        // Get product by Id
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _repository.GetProductByIdAsync(id);
        }

        // Create a new product
        public async Task CreateProductAsync(Product product)
        {
            await _repository.CreateProductAsync(product);
        }

        // Update a product
        public async Task UpdateProductAsync(Product product)
        {
            await _repository.UpdateProductAsync(product);
        }

        // Delete a product
        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteProductAsync(id);
        }

        // Save the part number
        public async Task SaveProductPartNumberAsync(int id, string partNumber)
        {
            await _repository.SaveProductPartNumberAsync(id, partNumber);
        }
    }
}
