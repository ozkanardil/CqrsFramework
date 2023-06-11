using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CqrsFramework.Domain.Entities;

namespace CqrsFramework.Persistance.Configurations
{
    public class ShoppingCartEntityConfiguration : IEntityTypeConfiguration<ShoppingCartEntity>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartEntity> builder)
        {
            builder.ToTable("ShoppingCart");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserId).IsRequired();
            builder.Property(u => u.ProductId).IsRequired();
            builder.Property(u => u.ProductId).IsRequired();
            builder.Property(u => u.Quantity).IsRequired();
            builder.Property(u => u.Price).IsRequired();
            builder.Property(u => u.AddDate).IsRequired();
            builder.HasOne(p => p.Product).WithMany(c => c.ShoppingCart).HasForeignKey(p => p.ProductId);
        }
    }
}
