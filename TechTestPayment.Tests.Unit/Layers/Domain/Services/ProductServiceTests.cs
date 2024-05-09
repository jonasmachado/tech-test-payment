using Microsoft.Extensions.Logging;
using Moq;
using TechTestPayment.Cross.Exceptions;
using TechTestPayment.Domain.Abstractions.Repositories;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Domain.Entities;
using TechTestPayment.Domain.Services;

namespace TechTestPayment.Tests.Unit.Layers.Domain.Services
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IBaseRepository<Product>> _mockProductRepository;

        public ProductServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            Mock<ILogger<ProductService>> mockLogger = new();
            _mockProductRepository = new Mock<IBaseRepository<Product>>();
            _mockUnitOfWork.Setup(u => u.ProductRepository).Returns(_mockProductRepository.Object);

            _productService = new ProductService(_mockUnitOfWork.Object, mockLogger.Object);
        }

        [Fact]
        public async Task PerformStockReduction_MissingProductIds_ThrowsDatabaseException()
        {
            var productIds = new List<int> { 1, 2, 3 };
            var quantities = new Dictionary<int, double> { { 1, 10 }, { 2, 20 } };
            var products = new List<Product> { new() { Id = 1, ItemsRemaining = 15 } };

            _mockProductRepository.Setup(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Product, bool>>>()))
                .ReturnsAsync(products);

            var exception = await Assert.ThrowsAsync<DatabaseException>(() => _productService.PerformStockReduction(productIds, quantities));

            Assert.Equal("[POT-009] The product does not exist.", exception.Message);
        }

        [Fact]
        public async Task PerformStockReduction_AllProductIdsExistAndSufficientStock_ExecutesSuccessfully()
        {
            var productIds = new List<int> { 1, 2 };
            var quantities = new Dictionary<int, double> { { 1, 5 }, { 2, 15 } };
            var products = new List<Product>
            {
                new (){ Id = 1, ItemsRemaining = 10 },
                new (){ Id = 2, ItemsRemaining = 20 }
            };

            _mockProductRepository.Setup(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Product, bool>>>()))
                .ReturnsAsync(products);

            await _productService.PerformStockReduction(productIds, quantities);

            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }
    }
}
