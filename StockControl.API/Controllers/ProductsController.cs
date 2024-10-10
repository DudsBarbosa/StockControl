using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockControl.API.Handlers;
using StockControl.API.Models;
using StockControl.API.Services;

namespace StockControl.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductsController(ProductService productService, IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ProductService _productService = productService;

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {            
            try
            {
                return await _productService.GetProductsAsync();
            }
            catch (Exception ex)
            {
                await _mediator.Send(new LogErrorCommand(ex.Message, DateTime.Now));
                return []; // Return an empty list in case of an exception
            }
        }

        [HttpGet("{id}")]
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                return await _productService.GetProductByIdAsync(id);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new LogErrorCommand(ex.Message, DateTime.Now));
                return null; // Return null in case of an exception
            }
        }

        [HttpPost]
        public async Task CreateProductAsync(Product product)
        {
            try
            {
                await _productService.CreateProductAsync(product);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new LogErrorCommand(ex.Message, DateTime.Now));
            }
        }

        [HttpPost]
        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                await _productService.UpdateProductAsync(product);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new LogErrorCommand(ex.Message, DateTime.Now));
            }
        }

        [HttpPost]
        public async Task DeleteProductAsync(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new LogErrorCommand(ex.Message, DateTime.Now));
            }
        }

        [HttpPost]
        public async Task SaveProductPartNumberAsync(int id, string partNumber)
        {
            try
            {
                await _productService.SaveProductPartNumberAsync(id, partNumber);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new LogErrorCommand(ex.Message, DateTime.Now));
            }
        }
    }
}
