using AutoMapper;
using CqrsFramework.Persistance.Context;
using CqrsFramework.Tests.Shared;
using CqrsFramework.Application.Features.OrderItem.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Tests.Features
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
