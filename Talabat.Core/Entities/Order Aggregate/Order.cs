using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeleviryMethod deleviryMethod, ICollection<OrederItem> items, decimal subtotal , string paymentInteedID)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeleviryMethod = deleviryMethod;
            Items = items;
            Subtotal = subtotal;
            PaymentIntenId= paymentInteedID;

        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pinding;
        public  Address ShippingAddress { get; set; }

       // public int DeleviryMethodId { get; set; } //Forgin key
        public DeleviryMethod DeleviryMethod { get; set; } // nav prop [one]

        public ICollection<OrederItem> Items { get; set; }= new HashSet<OrederItem>(); //nav prop [many

        public decimal Subtotal { get; set; }   // sum of items price only without delavery cost



        //[NotMapped]  //to not map it in DB becouse its value calculated in run time
        //public decimal Total { get => Subtotal + DeleviryMethod.Cost; }   //dravied atrebute

        public decimal GetTotal()
            => Subtotal + DeleviryMethod.Cost; //this fun we can make it instade of last to lines

        public string PaymentIntenId { get; set; }  //for payment module
    }
}
