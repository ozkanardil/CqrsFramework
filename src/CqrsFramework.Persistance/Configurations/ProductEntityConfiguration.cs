using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CqrsFramework.Domain.Entities;

namespace CqrsFramework.Persistance.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Category_Id).IsRequired();
            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.Category_Id);
        }
    }
}
