using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.DTOs;
using Talabat.Api.Errors;
using Talabat.Core.Services;

namespace Talabat.Api.Controllers
{
    [Authorize]
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{basketID}")]  // POST : /api/Payments
        public async Task<ActionResult<CustomerBasketDto>>  CreateOrUpdatePaymentIntent (string basketID)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketID);
            if (basket == null) return BadRequest(new ApiResponse(400, " A Problem with your basket"));
            return Ok(basket);
        }

    }

}
