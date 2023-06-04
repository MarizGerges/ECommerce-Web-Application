using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Config
{
    public class DeleveryMethodConfigurations : IEntityTypeConfiguration<DeleviryMethod>
    {
        public void Configure(EntityTypeBuilder<DeleviryMethod> builder)
        {
            builder.Property(deleviryMethod => deleviryMethod.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
