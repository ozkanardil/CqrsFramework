using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Persistance.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Persistance.Context
{
    public class DatabaseContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public DatabaseContext(DbContextOptions dbContextOptions, 
                                IConfiguration configuration) 
                            : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<RoleEntity> Role { get; set; }
        public DbSet<UserRoleEntity> UserRole { get; set; }
        public DbSet<UserRoleVEntity> UserRoleV { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItem { get; set; }
        public DbSet<ShoppingCartEntity> ShoppingCart { get; set; }
        public DbSet<LogEntity> Logs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleVEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingCartEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LogEntityConfiguration());
        }
    }
}
