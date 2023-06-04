using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.DTOs
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }

        public int DeliveryMethodId { get; set;}

        public AddressDTO ShippingAddress { get; set; }
    }
}
