using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstAPI.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(12);
            builder.Property(p => p.Price).HasDefaultValue(50).HasColumnType("decimal(18,2)");
            builder.Property(p => p.DiscountPrice).HasDefaultValue(0).HasColumnType("decimal(18,2)");
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.CreateTime).HasDefaultValueSql("GETUTCDATE()");


        }
    }
}
