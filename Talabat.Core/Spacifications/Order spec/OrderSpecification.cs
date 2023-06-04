using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Spacifications.Order_spec
{
    public class OrderSpecification :BaseSpacification<Order>
    {
        public OrderSpecification(string Email) : base(o=>o.BuyerEmail == Email)
        {
            Includes.Add(o => o.DeleviryMethod);
            Includes.Add(o => o.Items);

            AddOrderByDesc(o => o.OrderDate);
        }
        public OrderSpecification( int id, string Email) : base(o => o.BuyerEmail == Email && o.Id==id)
        {
            Includes.Add(o => o.DeleviryMethod);
            Includes.Add(o => o.Items);

        }
    }
}
