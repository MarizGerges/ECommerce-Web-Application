using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
        //here must be Many Nav prop from product and we will configure this relationship using flount api method , i did not make this prop her becouse i do not need it in this business

    }
}
