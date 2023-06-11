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
    public class UserRoleVEntityConfiguration : IEntityTypeConfiguration<UserRoleVEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleVEntity> builder)
        {
            builder.ToTable("UserRoleV");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserId).IsRequired();
            builder.Property(u => u.RoleId).IsRequired();
            builder.Property(r => r.Role).IsRequired().HasMaxLength(50);

        }
    }
}
