using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Order_Aggregate;
using Address = Talabat.Core.Order_Aggregate.Address;

namespace Talabat.APIS.DTOs
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; } 
        public AddressDto shippingAddress { get; set; }
    }
}
