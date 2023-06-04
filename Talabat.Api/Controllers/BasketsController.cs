using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.DTOs;
using Talabat.Api.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;

namespace Talabat.Api.Controllers
{
    
    public class BasketsController :   BaseApiController
    {
        private readonly IBasketRepositry _basketRepositry;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepositry basketRepositry , IMapper mapper) 
        {
            _basketRepositry = basketRepositry;
            _mapper = mapper;
        }

        [HttpGet("{id}")]   //this action for get and reCreate
        public async Task<ActionResult <CustomerBasket>> GetBasket (string Id)
        {
            var  basket = await _basketRepositry.GetBasketAsync (Id);
            return basket ?? new CustomerBasket(Id);
        }

        [HttpPost] // this for create or update
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createdOrUpdatedBasket = await _basketRepositry.UpdateBasketAsync(mappedBasket);
            if (createdOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
            return createdOrUpdatedBasket;

        }

        [HttpDelete] 
        public async Task DeleteBasket (string BasketId)
        {
            await _basketRepositry.DeleteBasketAsync (BasketId);
        }

    }
}
