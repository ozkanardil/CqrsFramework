using AutoMapper;
using CqrsFramework.Application.Features.OrderItem.Queries;
using CqrsFramework.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Shared;

namespace TestProject1.Features
{
    [Collection("TestCollection")]
    public class OrderItemTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public OrderItemTests()
        {
            _mapper = MapperBuilder.OrderItemMapper();
            _context = new DynamicDatabaseBuilder().CreateDatabaseContext();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

      
        [Fact]
        public async Task Get_OrderItem()
        {
            // Arrange
            var query = new GetOrderItemQuery { OrderId = 1 };
            var handler = new GetOrderItemQueryHandler(_mapper, _context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(1, result.Data.Count());
        }

     
    }
}
