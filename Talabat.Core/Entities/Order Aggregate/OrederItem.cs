using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class OrederItem : BaseEntity
    {
        public OrederItem() { }

        public OrederItem(ProductItemOreder product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOreder Product { get; set; }

        public decimal Price { get; set;}

        public int Quantity { get; set;}

    }
}
