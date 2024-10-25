using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpecifications
{
    public class OrderSpecifications : BaseSpecification<Order>
    {
        public OrderSpecifications(string Email) : base(o=>o.BuyerEmail == Email)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderBy(o => o.OrderDate);
        }
        
    }
}
