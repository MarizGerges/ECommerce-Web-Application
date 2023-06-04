using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set;}

        public int ProductBrandId { get; set;} //foregin key :not allow null
        public ProductBrand productBrand { get; set; } //navigation prop [one]

        public int ProductTypeId { get; set; } //foregin key :not allow null
        public ProductType productType { get; set; } //navigation prop [one]
    }
}
