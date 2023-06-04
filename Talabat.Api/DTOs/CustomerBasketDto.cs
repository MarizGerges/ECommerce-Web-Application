using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.Api.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; } 

        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

        public int? DeliveryMethodID { get; set; }

        public decimal ShippingCost { get; set; }

    }
}
