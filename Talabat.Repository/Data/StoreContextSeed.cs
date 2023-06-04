using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static  class StoreContextSeed
    {
        
        public static async Task SeedAsync( StoreContext context)
        {
            if (!context.productsBrands.Any())
            {
                var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brands is not null && brands.Count > 0)
                {
                    foreach (var item in brands)
                        context.Set<ProductBrand>().AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.productsTypes.Any())
            {
                var typeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                if (types is not null && types.Count > 0)
                {
                    foreach (var item in types)
                        context.Set<ProductType>().AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.products.Any())
            {
                var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products is not null && products.Count > 0)
                {
                    foreach (var item in products)
                        context.Set<Product>().AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }


            if (!context.deleviryMethods.Any())
            {
                var deleviryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deleviryMethods = JsonSerializer.Deserialize<List<DeleviryMethod>>(deleviryMethodsData);
                if (deleviryMethods is not null && deleviryMethods.Count > 0)
                {
                    foreach (var item in deleviryMethods)
                        context.Set<DeleviryMethod>().AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
