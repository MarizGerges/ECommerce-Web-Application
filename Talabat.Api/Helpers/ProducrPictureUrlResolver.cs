using AutoMapper;
using AutoMapper.Execution;
using System.Security.Cryptography.X509Certificates;
using Talabat.Api.DTOs;
using Talabat.Core.Entities;

namespace Talabat.Api.Helpers
{
    public class ProducrPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProducrPictureUrlResolver( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
          
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
            }

            return string.Empty ;
        }
    }
}
