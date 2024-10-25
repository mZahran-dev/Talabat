using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Order_Aggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.APIS.Controllers
{
    
    public class OrdersControllers : APIBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersControllers(IOrderService orderService ,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
         public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
         {
            var address = _mapper.Map<AddressDto, Address>(orderDto.shippingAddress);
            var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);
            if (order is null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
         }

        [HttpGet] //api/Orders?email=userEmail
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUsers(string UserEmail)
        {
            var order = await _orderService.GetOrdersForUsersAsync(UserEmail);
            return Ok(order);
        }

        [HttpGet("id")]
        public async Task<ActionResult<Order>> GetOrderByIdForUser(int id, string buyerEmail)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(id, buyerEmail);
            if (order is null) return NotFound(new ApiResponse(404));
            return Ok(order);
        }

    }
}
