using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(IBasketRepository basketRepository, IGenericRepository<Product> ProductRepo, IGenericRepository<DeliveryMethod> deliveryMethod, IGenericRepository<Order> OrderRepo) 
        {
            _basketRepository = basketRepository;
            _productRepo = ProductRepo;
            _deliveryMethodRepo = deliveryMethod;
            _orderRepo = OrderRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string baskitId, int deliveryMethodId, Address shippingAddress)
        {
            // 1. Get Basket From basket repo
            var basket = await _basketRepository.GetBasketAsync(baskitId);
            
            // 2. Get Selected Items at Basket from products repo

            var orderItems = new List<OrderItem>();
            if (basket?.Items?.Count > 0) 
            {
                foreach(var item in basket.Items)
                {
                    // product from Database
                    var product = await _productRepo.GetAsync(item.Id);
                    var ProductItemOrder = new ProductItemOrder(item.Id, product.Name, product.PictureUrl);
                    var OrderItem = new OrderItem(ProductItemOrder, product.Price, item.Quantity);

                    orderItems.Add(OrderItem);
                }
            }  





            // 3. Calc SupTotal
            var supTotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

            // 4. Get Delivery Method From DeliveryMethod Repo
            var deliveryMethod = await _deliveryMethodRepo.GetAsync(deliveryMethodId);

            // 5. Create Order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, supTotal);
            await _orderRepo.AddAsync(order); 

            // 6. Save To Database ......

            




        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUsersAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
