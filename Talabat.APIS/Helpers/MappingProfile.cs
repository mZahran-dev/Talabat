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
        }
    }
}
