using AutoMapper;
using FluentValidation;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Dto.Http.Response;
using TechTestPayment.Application.Services.Abstractions;
using TechTestPayment.Cross.Enums;
using TechTestPayment.Domain.Abstractions.UOW;
using TechTestPayment.Domain.Entities;

namespace TechTestPayment.Application.Services.Concretes
{
    public class ProductService(IUnitOfWork unitOfWork, IValidator<RegisterProductRequest> registerProductValidator, IMapper mapper) : BaseService, IProductService
    {
        public async Task<RegisterProductResponse> Register(RegisterProductRequest request)
        {
            ValidateRequest(registerProductValidator, request);
            await unitOfWork.ProductRepository.ThrowIfExistsAsync(x => x.Name == request.Name, ErrorCodes.ProductAlreadyExists);

            var product = mapper.Map<Product>(request);

            await unitOfWork.ProductRepository.InsertAsync(product);
            await unitOfWork.SaveChangesAsync();

            return new RegisterProductResponse() { Id = product.Id };
        }
    }
}
