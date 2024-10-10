using StockControl.API.Models;
using StockControl.API.Repositories;

namespace StockControl.API.Services
{
    public class OrderService(IOrderRepository orderRepository) : IOrderRepository
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task CreateOrderAsync(Order order)
        {
            await _orderRepository.CreateOrderAsync(order);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orderRepository.GetOrdersAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersConsumed(DateTime dateTime)
        {
            return await _orderRepository.GetOrdersConsumed(dateTime);
        }
    }
}
