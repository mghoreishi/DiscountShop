using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Domain.AggregateModel.CategoryAggregate;

namespace Shopping.Infrastructure.EntityConfigurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category").HasKey(k => k.Id);

            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.CategoryName).HasColumnName("name").IsRequired().HasMaxLength(50)
                .HasConversion(p => p.Value, p => CategoryName.Create(p).Value);

        }
    }
}
