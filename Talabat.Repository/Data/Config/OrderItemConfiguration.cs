using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrederItem>
    {
        public void Configure(EntityTypeBuilder<OrederItem> builder)
        {
            builder.OwnsOne(orederItem => orederItem.Product, product => product.WithOwner());
            builder . Property(orderItem=>orderItem.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
