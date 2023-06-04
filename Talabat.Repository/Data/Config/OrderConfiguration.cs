using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net.NetworkInformation;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner()); //1:1 [total]
            builder.Property(o => o.Status)
            .HasConversion(
                        OStatus=>OStatus.ToString(),
                        OStatus =>(OrderStatus) Enum.Parse(typeof(OrderStatus), OStatus)        

                );

            builder.Property(o => o.Subtotal)
                .HasColumnType("decimal(18,2)");


        }
    }
}
