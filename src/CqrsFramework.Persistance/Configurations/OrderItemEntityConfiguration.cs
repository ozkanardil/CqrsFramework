using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CqrsFramework.Domain.Entities;

namespace CqrsFramework.Persistance.Configurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItemEntity>
    {
        public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            builder.ToTable("OrderItem");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.OrderId).IsRequired();
            builder.Property(c => c.ProductId).IsRequired();
            builder.Property(c => c.Quantity).IsRequired();
            builder.Property(c => c.Price).IsRequired();
            builder.HasOne(p => p.Product).WithOne(c => c.OrderItem);
        }
    }
}
