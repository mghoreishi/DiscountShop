using Discounting.Domain.AggregateModel.DiscountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Discounting.Infrastructure.EntityConfigurations
{
    public class DiscountEntityTypeConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("discount").HasKey(k => k.Id);

            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.ShopId).HasColumnName("shop_id");

            builder.Property(p => p.DiscountName).HasColumnName("name").IsRequired().HasMaxLength(50)
                .HasConversion(p => p.Value, p => DiscountName.Create(p).Value);

            builder.Property(p => p.DiscountDescription).HasColumnName("description").IsRequired().HasMaxLength(300)
               .HasConversion(p => p.Value, p => DiscountDescription.Create(p).Value);
        }
    }
}
