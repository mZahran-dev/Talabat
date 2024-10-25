using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIS.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregate;

namespace Talabat.APIS.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.Product.ProductUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.Product.ProductUrl}";
            }
            return string.Empty;
        }
    }
}
