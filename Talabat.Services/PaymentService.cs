using Microsoft.Extensions.Configuration;
using Stripe;
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

namespace Talabat.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepositry _basketRepositry;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration 
            , IBasketRepositry basketRepositry
            ,IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepositry = basketRepositry;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var basket = await _basketRepositry.GetBasketAsync(basketId);
            if(basket is null) return null;

            var shippingPrice = 0m;
            if(basket.DeliveryMethodID.HasValue)
            {
                var deliveryMethod=await _unitOfWork.Repositery<DeleviryMethod>().GetByIdAsync(basket.DeliveryMethodID.Value);

                basket.ShippingCost = deliveryMethod.Cost;

                shippingPrice = deliveryMethod.Cost;
            }

            if(basket?.Items?.Count() > 0)
            {
                foreach( var item in basket.Items)
                {
                    var product = await _unitOfWork.Repositery<Talabat.Core.Entities.Product>().GetByIdAsync(item.Id);
                    if(item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            var service = new PaymentIntentService();

            PaymentIntent paymentIntent ;


            if(string.IsNullOrEmpty(basket.PaymentIntentId)) //create payment intent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount=(long)basket.Items.Sum(item=>item.Price * item.Quantity * 100) ,
                    Currency="usd",
                    PaymentMethodTypes=new List<string>() { "card"}
                };
                paymentIntent= await service.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else //update paymentIntent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) 
                    
                };
                await service.UpdateAsync(basket.PaymentIntentId,options);
            }

            await _basketRepositry.UpdateBasketAsync(basket);

            return basket;
        }
    }
}
