using AutoMapper;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Domain.Entities;

namespace TechTestPayment.Application.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<RegisterProductRequest, Product>();
        }
    }
}
