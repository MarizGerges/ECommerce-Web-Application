using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositries;
using Talabat.Core.Services;
using Talabat.Core.Spacifications.Order_spec;

namespace Talabat.Services
{
    public class OrederService : IOrderService
    {
        private readonly IBasketRepositry _basketRepositry;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrederService(
            IBasketRepositry basketRepositry,
            IUnitOfWork unitOfWork,
            IPaymentService paymentService
            ) 
        {
            _basketRepositry = basketRepositry;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string basketId, int deleveryMethodId, Address shippingAddress)
        {
            //1.get Basket from repo
            var basket =await _basketRepositry.GetBasketAsync(basketId);

            //2. get selevted items at basket from repo
            var orderItems=new List<OrederItem>();
            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repositery<Product>().GetByIdAsync(item.Id);
                    var productItemOrdedred = new ProductItemOreder(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrederItem(productItemOrdedred,product.Price , item.Quantity) ;
                    orderItems.Add(orderItem);
                }
            }

            //3.calculate sibtotal
            var subtotal=orderItems.Sum(item=>item.Price *item.Quantity);

            //4.get delevery mithod from deleveryMithod  repo
            var deliveryMethod = await _unitOfWork.Repositery<DeleviryMethod>().GetByIdAsync(deleveryMethodId);

            //5. create order
            var spec = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repositery<Order>().GetEntityWithSpecAsync(spec);
            if(existingOrder != null)
            {
                _unitOfWork.Repositery<Order>().Delete(existingOrder);

                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }
            var order= new Order(BuyerEmail,shippingAddress,deliveryMethod ,orderItems, subtotal,basket.PaymentIntentId);
            await _unitOfWork.Repositery<Order>().Add(order);

            //6. save to DB
            var result= await _unitOfWork.Complete();
            if (result <= 0) return null;

            return order;


        }

       

        public async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderSpecification(orderId, buyerEmail);
            var order= await _unitOfWork.Repositery<Order>().GetEntityWithSpecAsync(spec);
            return order;

        }

        public async Task<IReadOnlyList<Order>> GetOrderForSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var orders= await _unitOfWork.Repositery<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }

        public Task<IReadOnlyList<DeleviryMethod>> GetDeliveryMithodsAsync()
        {
            var deliveryMithods = _unitOfWork.Repositery<DeleviryMethod>().GetAllAsync();
            return deliveryMithods;
        }
    }
}
