using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockControl.API.Handlers;
using StockControl.API.Models;
using StockControl.API.Services;

namespace StockControl.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController(IMediator mediator, OrderService orderService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly OrderService _orderService = orderService;

        [HttpPost]
        public async Task CreateOrderAsync(Order order)
        {
            try
            {
                await _orderService.CreateOrderAsync(order);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new LogErrorCommand(ex.Message, DateTime.Now));
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            try
            {
                return await _orderService.GetOrdersAsync();
            }
            catch (Exception ex)
            {
                await _mediator.Send(new LogErrorCommand(ex.Message, DateTime.Now));
                return []; // Return an empty list in case of an exception
            }
        }

        [HttpGet("{dateTime}")]
        public async Task<IEnumerable<Order>> GetOrdersConsumed(DateTime dateTime)
        {
            try
            {
                return await _orderService.GetOrdersConsumed(dateTime);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new LogErrorCommand(ex.Message, DateTime.Now));
                return []; // Return an empty list in case of an exception
            }
        }
    }
}
