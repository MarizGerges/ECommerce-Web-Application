using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.Api.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrederItem, OrederItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrederItem source, OrederItemDto destination, string destMember, ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.Product.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
