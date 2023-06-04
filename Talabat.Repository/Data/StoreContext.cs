using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Repository.Data.Config;

namespace Talabat.Repository.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }

        public DbSet<Product>products { get; set; }
        public DbSet<ProductBrand> productsBrands { get; set; }

        public DbSet<ProductType> productsTypes { get; set; }    

        public DbSet<Order> orders { get; set; }
        public DbSet<OrederItem> orederItems { get; set; }
        public DbSet<DeleviryMethod> deleviryMethods { get; set; }





    }
}
