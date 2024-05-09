using AutoMapper;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Domain.Entities;

namespace TechTestPayment.Application.Mappers
{
    public class SellerProfile : Profile
    {
        public SellerProfile()
        {
            CreateMap<RegisterSellerRequest, Seller>();
        }
    }
}
