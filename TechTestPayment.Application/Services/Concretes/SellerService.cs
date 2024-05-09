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
    public class SellerService(IUnitOfWork unitOfWork, IValidator<RegisterSellerRequest> registerSellerValidator, IMapper mapper) : BaseService, ISellerService
    {
        public async Task<RegisterSellerResponse> Register(RegisterSellerRequest request)
        {
            ValidateRequest(registerSellerValidator, request);
            await unitOfWork.SellerRepository.ThrowIfExistsAsync(x => x.Cpf == request.Cpf, ErrorCodes.SellerAlreadyExists);

            var seller = mapper.Map<Seller>(request);

            await unitOfWork.SellerRepository.InsertAsync(seller);
            await unitOfWork.SaveChangesAsync();

            return new RegisterSellerResponse() { Id = seller.Id };
        }
    }
}
