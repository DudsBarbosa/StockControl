using StockControl.API.Models;

namespace StockControl.API.Repositories
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetOrdersAsync();

        public Task<IEnumerable<Order>> GetOrdersConsumed(DateTime dateTime);
        public Task CreateOrderAsync(Order order);
    }
}
