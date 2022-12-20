using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.AggregateModel.ShopAggregate;


namespace Shopping.Infrastructure.EntityConfigurations
{
    public class ShopEntityTypeConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable("shop").HasKey(k => k.Id);

            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.CategoryId).HasColumnName("category_id");

            builder.Property(p => p.DiscountCount).HasColumnName("discount_count").HasDefaultValue(0);

            builder.Property(p => p.ShopName).HasColumnName("name").IsRequired().HasMaxLength(50)
                .HasConversion(p => p.Value, p => ShopName.Create(p).Value);

            builder.Property(p => p.ShopDescription).HasColumnName("description").IsRequired().HasMaxLength(300)
               .HasConversion(p => p.Value, p => ShopDescription.Create(p).Value);

            builder.Property(p => p.Address).HasColumnName("address").IsRequired().HasMaxLength(100)
               .HasConversion(p => p.Value, p => Address.Create(p).Value);

            builder.Property(p => p.Phone).HasColumnName("phone").IsRequired().HasMaxLength(121)
              .HasConversion(p => p.Value, p => Phone.Create(p).Value);

        }
    }
}
