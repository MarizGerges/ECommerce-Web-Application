using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Spacifications;
using Talabat.Core.Entities;
using Talabat.Api.Helpers;

namespace Talabat.Core.Spacifications
{
    public class ProductWithBrandAndTypeSpacificatoin :BaseSpacification<Product>
    {
        //this constructor is used for GetAllProduct

        public ProductWithBrandAndTypeSpacificatoin( ProductSpecPrams specPrams) 
            : base (p=>
                       (string.IsNullOrEmpty(specPrams.Search)|| p.Name.ToLower().Contains(specPrams.Search))&&
                       (!specPrams.BrandId.HasValue || p.ProductBrandId == specPrams.BrandId.Value)&&
                       (!specPrams.TypeId.HasValue || p.ProductTypeId == specPrams.TypeId.Value)
             )
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);

            AddOrderBy(p => p.Name);

            if (!string.IsNullOrEmpty(specPrams.Sort))
            {
                switch (specPrams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p=>p.Price); 
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p=>p.Name); break;



                }
            }

            ApplayPagenation(specPrams.PageSize *(specPrams.PageIndex-1) , specPrams.PageSize);
        }

        //this constructor is used for GetByIdProduct
        public ProductWithBrandAndTypeSpacificatoin(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
        }
    }
}
