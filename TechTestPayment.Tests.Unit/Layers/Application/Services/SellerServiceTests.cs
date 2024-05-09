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
    public class SellerServiceTests
    {
        private readonly SellerService _sellerService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IValidator<RegisterSellerRequest>> _mockValidator;
        private readonly Mock<IMapper> _mapper;
        public SellerServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockValidator = new Mock<IValidator<RegisterSellerRequest>>();
            Mock<IBaseRepository<Seller>> mockSellerRepository = new();
            _mockUnitOfWork.Setup(u => u.SellerRepository).Returns(mockSellerRepository.Object);
            _mapper = new Mock<IMapper>();
            _sellerService = new SellerService(_mockUnitOfWork.Object, _mockValidator.Object, _mapper.Object);
        }

        [Fact]
        public async Task Register_WhenCalled_ReturnsRegisterSellerResponse()
        {
            var randomId = new Random().Next(1, 999);
            var request = new RegisterSellerRequest();
            var seller = new Seller() { Id = randomId };
            var response = new RegisterSellerResponse() { Id = randomId };

            _mockValidator.Setup(x => x.Validate(It.IsAny<RegisterSellerRequest>())).Returns(new ValidationResult());
            _mockUnitOfWork.Setup(x => x.SellerRepository.ThrowIfExistsAsync(It.IsAny<Expression<Func<Seller, bool>>>(), It.IsAny<ErrorCodes>()));
            _mapper.Setup(x => x.Map<Seller>(It.IsAny<RegisterSellerRequest>())).Returns(seller);
            _mockUnitOfWork.Setup(x => x.SellerRepository.InsertAsync(It.IsAny<Seller>()));
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync());
            _mapper.Setup(x => x.Map<RegisterSellerResponse>(It.IsAny<Seller>())).Returns(response);

            var result = await _sellerService.Register(request);

            Assert.Equal(response.Id, result.Id);
            _mockValidator.Verify(x => x.Validate(request), Times.Once);
            _mockUnitOfWork.Verify(x => x.SellerRepository.ThrowIfExistsAsync(It.IsAny<Expression<Func<Seller, bool>>>(), It.IsAny<ErrorCodes>()), Times.Once);
            _mapper.Verify(x => x.Map<Seller>(request), Times.Once);
            _mockUnitOfWork.Verify(x => x.SellerRepository.InsertAsync(seller), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
