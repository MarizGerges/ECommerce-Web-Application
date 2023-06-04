using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Api.Helpers;
using Talabat.Core.Entities;

namespace Talabat.Core.Spacifications
{
    public class ProductWithFilterationForCountSpecification : BaseSpacification<Product>
    {
        private ProductSpecPrams specPrams;

        public ProductWithFilterationForCountSpecification(ProductSpecPrams specPrams)
            : base(p =>
                       (string.IsNullOrEmpty(specPrams.Search) || p.Name.ToLower().Contains(specPrams.Search)) &&
                       (!specPrams.BrandId.HasValue || p.ProductBrandId == specPrams.BrandId.Value) &&
                       (!specPrams.TypeId.HasValue || p.ProductTypeId == specPrams.TypeId.Value)
             )
        {
            this.specPrams = specPrams;
        }
    }
}
