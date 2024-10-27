using AutoMapper;
using Talabat.APIS.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregate;

namespace Talabat.APIS.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.ProductCategory, O => O.MapFrom(S => S.ProductCategory.Name))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto,Address>();
            CreateMap<Order, OrderToReturnDto>()
                     .ForMember(d => d.DeliveryMethod, o=> o.MapFrom(s => s.DeliveryMethod.ShortName))
                     .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                     .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                     .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                     .ForMember(d => d.ProductUrl, o => o.MapFrom(s => s.Product.ProductUrl))
                     .ForMember(d => d.ProductUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
