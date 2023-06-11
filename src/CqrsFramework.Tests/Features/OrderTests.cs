using AutoMapper;
using CqrsFramework.Application.Features.Order.Commands;
using CqrsFramework.Application.Features.Order.Models;
using CqrsFramework.Persistance.Context;
using CqrsFramework.Tests.Shared;
using CqrsFramework.Application.Features.Order.Commands;
using CqrsFramework.Application.Features.Order.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Tests.Features
{
    [Collection("TestCollection")]
    public class OrderTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public OrderTests()
        {
            _mapper = MapperBuilder.OrderMapper();
            _context = new DynamicDatabaseBuilder().CreateDatabaseContext();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task Create_Order()
        {
            // Arrange
            var newOrder = new CreateOrderCommand
            {
                UserId = 1,
                Amount = 121,
                PaymentMethod = "Cash",
            };

            var handler = new CreateOrderCommandHandler(_mapper, _context);

            var getQuery = new GetOrderQuery { UserId = 1 };

            var getHandler = new GetOrderQueryHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(newOrder, CancellationToken.None);
            var getResult = await getHandler.Handle(getQuery, CancellationToken.None);
            var newAddedOrder = _context.Orders.Where(o => o.Id == 3).SingleOrDefault();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderResponse>(result);
            Assert.Equal(newAddedOrder.Id, result.Id);
            Assert.Equal(newAddedOrder.UserId, result.UserId);
            Assert.Equal(newAddedOrder.OrderDate, result.OrderDate);
            Assert.Equal(newAddedOrder.Status, result.Status);
            Assert.Equal(newAddedOrder.Amount, result.Amount);
            Assert.Equal(newAddedOrder.PaymentMethod, result.PaymentMethod);

            Assert.Equal(3, getResult.Data.Count());
        }


        [Fact]
        public async Task Get_Order()
        {
            // Arrange
            var query = new GetOrderQuery { UserId = 1 };
            var handler = new GetOrderQueryHandler(_mapper, _context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task Delete_Order()
        {
            // Arrange
            DeleteOrderCommand command = new DeleteOrderCommand(1);

            var handler = new DeleteOrderCommandHandler(_mapper, _context);

            var getQuery = new GetOrderQuery { UserId = 1 };
            var getHandler = new GetOrderQueryHandler(_mapper, _context);


            //Act
            var resultDelete = await handler.Handle(command, CancellationToken.None);
            var getResult = await getHandler.Handle(getQuery, CancellationToken.None);

            // Assert

            //Assert.Null(result);
            Assert.Equal(true, resultDelete.Success);
            Assert.Equal(1, getResult.Data.Count());
        }
    }
}
