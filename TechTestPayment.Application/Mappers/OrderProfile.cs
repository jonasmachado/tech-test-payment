using AutoMapper;
using TechTestPayment.Application.Dto.Http.Request;
using TechTestPayment.Application.Dto.Http.Response;
using TechTestPayment.Domain.Entities;

namespace TechTestPayment.Application.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<RegisterOrderRequest.ProductItem, OrderProduct>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore());


            CreateMap<RegisterOrderRequest, Order>()
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId))
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.ProductsItems))
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Seller, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Order, GetOrderResponse>()
                .ForMember(dest => dest.ProductItems, opt => opt.MapFrom(src => src.OrderProducts.Select(op => new GetOrderProductItem
                {
                    Id = op.ProductId,
                    Name = op.Product!.Name,
                    Amount = op.Amount,
                    UnitPrice = op.Product!.Price
                })));
        }
    }
}
