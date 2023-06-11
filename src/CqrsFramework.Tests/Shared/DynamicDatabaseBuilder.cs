using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Tests.Shared
{
    public class DynamicDatabaseBuilder
    {

        public DatabaseContext CreateDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "test")
                .Options;

            var config = new Mock<IConfiguration>();
            config.Setup(x => x.GetSection("SomeSection")).Returns(new Mock<IConfigurationSection>().Object);

            var context = new DatabaseContext(options, config.Object);

            context.Categories.AddRange(new List<CategoryEntity>
            {
                new CategoryEntity { Id = 1, Name = "Category 1" },
                new CategoryEntity { Id = 2, Name = "Category 2" },
                new CategoryEntity { Id = 3, Name = "Category 3" }
            });

            context.Products.AddRange(new List<ProductEntity>
            {
                new ProductEntity { Id = 1, Name = "Product-1", Description = "Desc-1", Price = 11, Category_Id = 1 },
                new ProductEntity { Id = 2, Name = "Product-2", Description = "Desc-2", Price = 12, Category_Id = 1 },
                new ProductEntity { Id = 3, Name = "Product-3", Description = "Desc-3", Price = 13, Category_Id = 1 }
            });

            context.Orders.AddRange(new List<OrderEntity>
            {
                new OrderEntity { Id = 1, OrderDate = new DateTime(2023, 04, 09), UserId=1, Status = 1, Amount=121, PaymentMethod="Cart" },
                new OrderEntity { Id = 2, OrderDate = new DateTime(2023, 04, 11), UserId=1, Status = 1, Amount=122, PaymentMethod="Cart" },
            });

            context.OrderItem.AddRange(new List<OrderItemEntity>
            {
                new OrderItemEntity { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1, Price=11 },
                new OrderItemEntity { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1, Price=12 },
                new OrderItemEntity { Id = 3, OrderId = 2, ProductId = 1, Quantity = 2, Price=11 },
            });

            context.User.AddRange(new List<UserEntity>
            {
                new UserEntity { Id = 1, Name = "Name-1", Surname = "Surname-1", Email = "gmail1", Password="111", Status = 1 },
                new UserEntity { Id = 2, Name = "Name-2", Surname = "Surname-2", Email = "gmail2", Password="222", Status = 1 },
                new UserEntity { Id = 3, Name = "Name-3", Surname = "Surname-3", Email = "gmail3", Password="333", Status = 1 },
            });

            context.SaveChanges();

            return context;
        }



    }
}

