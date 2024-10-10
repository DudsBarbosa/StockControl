using Dapper;
using StockControl.API.Models;

namespace StockControl.API.Repositories
{
    public class ProductRepository(DataContext dataContext) : IProductRepository
    {
        private readonly DataContext _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        public async Task CreateProductAsync(Product product)
        {
            using var _connection = _dataContext.CreateConnection();
            await _connection.ExecuteAsync("INSERT INTO Products (Description, StockQuantity, AverageCostPrice, PartNumber) VALUES (@Description, @StockQuantity, @AverageCostPrice, @PartNumber)", product);
        }

        public async Task DeleteProductAsync(int id)
        {
            using var _connection = _dataContext.CreateConnection();
            await _connection.ExecuteAsync("DELETE FROM Products WHERE Id = @Id", new { Id = id });
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            using var _connection = _dataContext.CreateConnection();
            var product = await _connection.QuerySingleOrDefaultAsync<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with Id {id} not found.");
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using var _connection = _dataContext.CreateConnection();
            return await _connection.QueryAsync<Product>("SELECT * FROM Products");
        }

        public async Task UpdateProductAsync(Product product)
        {
            using var _connection = _dataContext.CreateConnection();
            await _connection.ExecuteAsync("UPDATE Products SET Description = @Description, StockQuantity = @StockQuantity, AverageCostPrice = @AverageCostPrice, PartNumber = @PartNumber WHERE Id = @Id", product);

            // if @StockQuantity is different from the current stock quantity, call ConsuptionService to update stock quantity
        }

        public async Task SaveProductPartNumberAsync(int id, string partNumber)
        {
            using var _connection = _dataContext.CreateConnection();
            await _connection.ExecuteAsync("UPDATE Products SET PartNumber = @PartNumber WHERE Id = @Id", new { Id = id, PartNumber = partNumber });
        }
    }
}
