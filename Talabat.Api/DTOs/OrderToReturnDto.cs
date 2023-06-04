using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public string Status { get; set; } 
        public Address ShippingAddress { get; set; }
        //public DeleviryMethod DeleviryMethod { get; set; } // nav prop [one]
        public string DeleviryMethod { get; set; }

        public decimal DeleviryMethodCost { get; set; }
        public ICollection<OrederItemDto> Items { get; set; } 

        public decimal Subtotal { get; set; }   // sum of items price only without delavery cost

        public decimal Total { get; set; }

        public string PaymentIntenId { get; set; } = string.Empty; //for payment module

    }
}
