using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Spacifications.Order_spec
{
    public class OrderWithPaymentIntentSpecification :BaseSpacification<Order>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId ) :base(o=>o.PaymentIntenId==paymentIntentId)
        {
            
        }
    }
}
