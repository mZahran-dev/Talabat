using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregate;

namespace Talabat.Core.Services.Contract
{
    public interface IOrderService
    {
        public Task<Order> CreateOrderAsync(string buyerEmail, string baskitId, int deliveryMethod, Address shippingAddress);
        public Task<IReadOnlyList<Order>> GetOrdersForUsersAsync(string buyerEmail);
        Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail);
    }
}
