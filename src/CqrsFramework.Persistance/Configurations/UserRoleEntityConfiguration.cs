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
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserId).IsRequired();
            builder.Property(u => u.RoleId).IsRequired();
            builder.HasOne(p => p.Role).WithMany(c => c.UserRole).HasForeignKey(p => p.RoleId);
        }
    }
}
