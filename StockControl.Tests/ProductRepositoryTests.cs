using Moq;
using StockControl.API.Models;
using StockControl.API.Repositories;
using StockControl.API.Services;
using System.Data;

namespace StockControl.Tests
{
    public class ProductRepositoryTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IDbConnection> _mockConnection;
        private readonly Mock<IProductRepository> _mockRepository;

        public ProductRepositoryTests()
        {
            _mockConnection = new Mock<IDbConnection>();
            _mockRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetProductsAsync_ReturnsProducts()
        {
            // Arrange
            var products = new List<Product>
                    {
                        new() { Id = 1, Description = "Description 1", StockQuantity = 10, AverageCostPrice = 100.0m },
                        new() { Id = 2, Description = "Description 2", StockQuantity = 20, AverageCostPrice = 200.0m }
                    };

            _mockRepository.Setup(x => x.GetProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetProductsAsync();

            // Assert
            Assert.Equal(products, result);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Description = "Description 1", StockQuantity = 10, AverageCostPrice = 100.0m };

            _mockRepository.Setup(x => x.GetProductByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.Equal(product, result);
        }

        [Fact]
        public async Task CreateProductAsync_CreatesProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Description = "Description 1", StockQuantity = 10, AverageCostPrice = 100.0m };

            _mockRepository.Setup(x => x.CreateProductAsync(product));

            // Act
            await _productService.CreateProductAsync(product);

            // Assert
            _mockRepository.Verify(x => x.CreateProductAsync(product), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_UpdatesProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Description = "Description 1", StockQuantity = 9, AverageCostPrice = 90.0m };

            _mockRepository.Setup(x => x.UpdateProductAsync(product));

            // Act
            await _productService.UpdateProductAsync(product);

            // Assert
            _mockRepository.Verify(x => x.UpdateProductAsync(product), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_DeletesProduct()
        {
            // Arrange
            var id = 1;

            _mockRepository.Setup(x => x.DeleteProductAsync(id));

            // Act
            await _productService.DeleteProductAsync(id);

            // Assert
            _mockRepository.Verify(x => x.DeleteProductAsync(id), Times.Once);
        }
    }
}
