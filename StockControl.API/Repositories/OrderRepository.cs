using Dapper;
using StockControl.API.Models;

namespace StockControl.API.Repositories
{
    public class OrderRepository(DataContext dataContext) : IOrderRepository
    {
        private readonly DataContext _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        public async Task CreateOrderAsync(Order order)
        {
            using var _connection = _dataContext.CreateConnection();
            var product = await _connection.QuerySingleOrDefaultAsync<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = order.ProductId }) ?? throw new KeyNotFoundException($"Product with Id {order.ProductId} not found.");

            ValidaUnavailableQuantity(product);
            ValidateStockQuantity(order, product);

            await _connection.ExecuteAsync("INSERT INTO Orders (ProductId, Quantity, Date, Value) VALUES (@ProductId, @Quantity, @Date, @Value)", order);
        }
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            using var _connection = _dataContext.CreateConnection();
            return await _connection.QueryAsync<Order>("SELECT * FROM Orders");
        }

        private static void ValidaUnavailableQuantity(Product product)
        {
            if (product.StockQuantity <= 0)
            {
                throw new Exception($"Insufficient stock for productId {product.Id}");
            }
        }

        private static void ValidateStockQuantity(Order order, Product product)
        {
            if (order.Quantity > product.StockQuantity)
            {
                throw new InvalidOperationException($"Insufficient stock quantity for productId {product.Id}. Current quantity: {product.StockQuantity}");
            }
        }
        public async Task<IEnumerable<Order>> GetOrdersConsumed(DateTime dateTime)
        {
            using var _connection = _dataContext.CreateConnection();
            return await _connection.QueryAsync<Order>("SELECT O.ProductId, O.Date, SUM(O.Quantity) AS Quantity, AVG(P.AverageCostPrice) - AVG(O.Value) AS Value FROM Orders O INNER JOIN Products P ON O.ProductId = P.Id WHERE Date = @Date GROUP BY ProductId, Date", new { Date = dateTime });
        }
    }
}
