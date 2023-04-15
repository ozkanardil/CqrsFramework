using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Shared
{
    public sealed class TestDatabaseContext
    {
        private static readonly TestDatabaseContext _instance = new TestDatabaseContext();

        public DatabaseContext Context { get; }

        private TestDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "test")
                .Options;

            var config = new Mock<IConfiguration>();
            config.Setup(x => x.GetSection("SomeSection")).Returns(new Mock<IConfigurationSection>().Object);

            Context = new DatabaseContext(options, config.Object);
            Context.Categories.AddRange(new List<CategoryEntity>
                {
                    new CategoryEntity { Id = 1, Name = "Category 1" },
                    new CategoryEntity { Id = 2, Name = "Category 2" },
                    new CategoryEntity { Id = 3, Name = "Category 3" }
                });
            Context.SaveChanges();
        }

        public static TestDatabaseContext Instance
        {
            get { return _instance; }
        }
    }

}
