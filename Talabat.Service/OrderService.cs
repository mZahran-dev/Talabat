using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.OrderSpecifications;
using Talabat.Core.UnitOfWork.Contract;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        //private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork) 
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            //_productRepo = ProductRepo;
            //_deliveryMethodRepo = deliveryMethod;
            //_orderRepo = OrderRepo;
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
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    var ProductItemOrder = new ProductItemOrder(item.Id, product.Name, product.PictureUrl);
                    var OrderItem = new OrderItem(ProductItemOrder, product.Price, item.Quantity);

                    orderItems.Add(OrderItem);
                }
            }


            // 3. Calc SupTotal
            var supTotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

            // 4. Get Delivery Method From DeliveryMethod Repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);

            // 5. Create Order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, supTotal);
            await _unitOfWork.Repository<Order>().AddAsync(order);

            // 6. Save To Database ......
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            
            return order;



        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUsersAsync(string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var spec = new OrderSpecifications(buyerEmail);
            var orders = orderRepo.GetAllSpecAsync(spec);
            return orders;
            
        }
    }
}
