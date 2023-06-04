using AutoMapper;
using Talabat.Api.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.productBrand,o=>o.MapFrom(s=>s.productBrand.Name))
                .ForMember(d => d.productType, o => o.MapFrom(s => s.productType.Name))
                .ForMember(d=>d.PictureUrl , o=>o.MapFrom<ProducrPictureUrlResolver>());

            CreateMap<Talabat.Core.Entities.Identity.Address, AddressDTO>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDTO,BasketItem>();
            CreateMap<AddressDTO, Talabat.Core.Entities.Order_Aggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d=>d.DeleviryMethod , o=>o.MapFrom(s=>s.DeleviryMethod.ShortName))
                .ForMember(d=>d.DeleviryMethodCost, o=>o.MapFrom(s=>s.DeleviryMethod.Cost));

            CreateMap<OrederItem, OrederItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}
