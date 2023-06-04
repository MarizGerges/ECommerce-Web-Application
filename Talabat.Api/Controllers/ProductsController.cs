using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.DTOs;
using Talabat.Api.Errors;
using Talabat.Api.Helpers;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;
using Talabat.Core.Spacifications;

namespace Talabat.Api.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       // [Authorize/*(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)*/]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecPrams SpecPrams)
        {
            var spec = new ProductWithBrandAndTypeSpacificatoin(SpecPrams);
            var products=await _unitOfWork.Repositery<Product>().GetAllWithSpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFilterationForCountSpecification(SpecPrams);

            var count =await _unitOfWork.Repositery<Product>().GetCountWithAsync(countSpec);

            return Ok(new Pagination<ProductToReturnDto>(SpecPrams.PageIndex , SpecPrams.PageSize,count , data));

        }
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndTypeSpacificatoin(id);

            var product = await _unitOfWork.Repositery<Product>().GetEntityWithSpecAsync(spec);

            if (product is null) return NotFound(new ApiResponse(404));


            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));

        }


        [HttpGet ("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands= await _unitOfWork.Repositery<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _unitOfWork.Repositery<ProductType>().GetAllAsync();
            return Ok(types);
        }
    }
}
