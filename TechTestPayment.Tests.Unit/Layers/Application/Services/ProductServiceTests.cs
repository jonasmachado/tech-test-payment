using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IValidator<RegisterProductRequest>> _mockValidator;
        private readonly Mock<IMapper> _mapper;

        public ProductServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockValidator = new Mock<IValidator<RegisterProductRequest>>();
            Mock<IBaseRepository<Product>> mockProductRepository = new();
            _mockUnitOfWork.Setup(u => u.ProductRepository).Returns(mockProductRepository.Object);
            _mapper = new Mock<IMapper>();

            _productService = new ProductService(_mockUnitOfWork.Object, _mockValidator.Object, _mapper.Object);
        }

        [Fact]
        public async Task Register_WhenCalled_ReturnsRegisterProductResponse()
        {
            var randomId = new Random().Next(1, 999);
            var request = new RegisterProductRequest();
            var product = new Product() { Id = randomId };
            var response = new RegisterProductResponse() { Id = randomId };

            _mockValidator.Setup(x => x.Validate(It.IsAny<RegisterProductRequest>())).Returns(new ValidationResult());
            _mockUnitOfWork.Setup(x => x.ProductRepository.ThrowIfExistsAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<ErrorCodes>()));
            _mapper.Setup(x => x.Map<Product>(It.IsAny<RegisterProductRequest>())).Returns(product);
            _mockUnitOfWork.Setup(x => x.ProductRepository.InsertAsync(It.IsAny<Product>()));
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync());
            _mapper.Setup(x => x.Map<RegisterProductResponse>(It.IsAny<Product>())).Returns(response);
            var result = await _productService.Register(request);

            Assert.Equal(response.Id, result.Id);
            _mockValidator.Verify(x => x.Validate(request), Times.Once);
            _mockUnitOfWork.Verify(x => x.ProductRepository.ThrowIfExistsAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<ErrorCodes>()), Times.Once);
            _mapper.Verify(x => x.Map<Product>(request), Times.Once);
            _mockUnitOfWork.Verify(x => x.ProductRepository.InsertAsync(product), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
