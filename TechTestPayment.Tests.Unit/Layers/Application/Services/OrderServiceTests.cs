using AutoBogus;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Dto.Http.Response;
using TechTestPayment.Application.Services.Concretes;
using TechTestPayment.Cross.Enums;
using TechTestPayment.Domain.Abstractions.Repositories;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Domain.Entities;

namespace TechTestPayment.Tests.Unit.Layers.Application.Services
{
    public class OrderServiceTests
    {
        private readonly OrderService _orderService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<TechTestPayment.Domain.Abstractions.Services.IProductService> _productService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IValidator<RegisterOrderRequest>> _registerOrderValidator;
        private readonly Mock<IDbContextTransaction> _mockTransaction;

        public OrderServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            Mock<IOrderRepository> mockOrderRepository = new();
            _mockUnitOfWork.Setup(u => u.OrderRepository).Returns(mockOrderRepository.Object);
            _mapper = new Mock<IMapper>();
            Mock<ILogger<OrderService>> logger = new();
            _productService = new Mock<TechTestPayment.Domain.Abstractions.Services.IProductService>();
            _registerOrderValidator = new Mock<IValidator<RegisterOrderRequest>>();
            Mock<IValidator<UpdateOrderRequest>> updateOrderValidator = new();
            _mockTransaction = new Mock<IDbContextTransaction>();

            _orderService = new OrderService(
                _mockUnitOfWork.Object,
                _mapper.Object,
                logger.Object,
                _productService.Object,
                _registerOrderValidator.Object,
                updateOrderValidator.Object);
        }

        [Fact]
        public async Task Register_WhenCalled_ReturnsRegisterOrderResponse()
        {
            var request = new AutoFaker<RegisterOrderRequest>().Generate();
            var order = new Order() { Id = 1 };
            var response = new RegisterOrderResponse { Id = order.Id };

            _registerOrderValidator.Setup(x => x.Validate(It.IsAny<RegisterOrderRequest>())).Returns(new ValidationResult());
            _mockUnitOfWork.Setup(x => x.BeginTransactionAsync()).Returns(Task.FromResult(_mockTransaction.Object));

            _mockUnitOfWork.Setup(x => x.SellerRepository.ThrowIfNotExistsAsync(It.IsAny<Expression<Func<Seller, bool>>>(), It.IsAny<ErrorCodes>()));

            _productService.Setup(x => x.PerformStockReduction(It.IsAny<List<int>>(), It.IsAny<Dictionary<int, double>>()));
            _mapper.Setup(x => x.Map<Order>(It.IsAny<RegisterOrderRequest>())).Returns(order);
            _mockUnitOfWork.Setup(x => x.OrderRepository.InsertAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            var result = await _orderService.Register(request);

            Assert.Equal(response.Id, result.Id);
            _mockUnitOfWork.Verify(x => x.SellerRepository.ThrowIfNotExistsAsync(It.IsAny<Expression<Func<Seller, bool>>>(), It.IsAny<ErrorCodes>()), Times.Once);
            _productService.Verify(x => x.PerformStockReduction(It.IsAny<List<int>>(), It.IsAny<Dictionary<int, double>>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.OrderRepository.InsertAsync(order), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
