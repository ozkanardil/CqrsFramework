using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CqrsFramework.Application.Features.Category.Commands;
using CqrsFramework.Application.Features.Category.Models;
using CqrsFramework.Application.Features.Category.Queries;
using CqrsFramework.Application.Features.Product.Commands;
using CqrsFramework.Application.Features.Product.Models;
using CqrsFramework.Application.Features.Product.Queries;
using CqrsFramework.Domain.Entities;
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
    public class ProductTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ProductTests()
        {
            _mapper = MapperBuilder.ProductMapper();
            //_context = TestDatabaseContext.Instance.Context;
            _context = new DynamicDatabaseBuilder().CreateDatabaseContext();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task Create_Product()
        {
            // Arrange
            var newProduct = new CreateProductCommand
            {
                Name = "New Product",
                Description = "New Description",
                Price = 4,
                Category_Id = 2,
            };

            var handler = new CreateProductCommandHandler(_mapper, _context);

            var getQuery = new GetProductQuery();
            var getHandler = new GetProductQueryHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(newProduct, CancellationToken.None);
            var getResult = await getHandler.Handle(getQuery, CancellationToken.None);
            var newAddedProduct = _context.Products.Find(4);
            // Assert

            //Assert.Null(result);

            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(newAddedProduct.Name, result.Name);
            Assert.Equal(newAddedProduct.Description, result.Description);
            Assert.Equal(newAddedProduct.Price, result.Price);
            Assert.Equal(newAddedProduct.Category_Id, result.Category_Id);

            Assert.Equal(4, getResult.Count());
        }

        [Fact]
        public async Task Update_Product()
        {
            // Arrange
            var product = new ProductEntity
            {
                Id = 4,
                Name = "Test Product",
                Description = "test Desc",
                Price = 12,
                Category_Id = 1
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var command = new UpdateProductCommand
            {
                Id = 4,
                Name = "Updated Product",
                Description = "Updated Product Desc",
                Price = 22,
                Category_Id = 2
            };

            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).State = EntityState.Detached;
            }

            var handler = new UpdateProductCommandHandler(_mapper, _context);


            var getQuery = new GetProductQuery();
            var getHandler = new GetProductQueryHandler(_mapper, _context);


            //Act
            var result = await handler.Handle(command, CancellationToken.None);
            var getResult = await getHandler.Handle(getQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Description, result.Description);
            Assert.Equal(command.Price, result.Price);
            Assert.Equal(command.Category_Id, result.Category_Id);

            Assert.NotEqual(product.Name, result.Name);
            Assert.NotEqual(product.Description, result.Description);
            Assert.NotEqual(product.Price, result.Price);
            Assert.NotEqual(product.Category_Id, result.Category_Id);

            Assert.Equal(4, getResult.Count());
        }

        [Fact]
        public async Task Get_Product()
        {
            // Arrange
            var query = new GetProductQuery();
            var handler = new GetProductQueryHandler(_mapper, _context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task Delete_Product()
        {
            // Arrange
            DeleteProductCommand command = new DeleteProductCommand(1);

            var existingProduct = _context.Products.Find(command.Id);

            var handler = new DeleteProductCommandHandler(_mapper, _context);


            var getQuery = new GetProductQuery();
            var getHandler = new GetProductQueryHandler(_mapper, _context);


            //Act
            var resultDelete = await handler.Handle(command, CancellationToken.None);
            var getResult = await getHandler.Handle(getQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(resultDelete);
            Assert.IsType<ProductResponse>(resultDelete);
            Assert.Equal(existingProduct.Id, resultDelete.Id);
            Assert.Equal(existingProduct.Name, resultDelete.Name);
            Assert.Equal(existingProduct.Description, resultDelete.Description);
            Assert.Equal(existingProduct.Price, resultDelete.Price);
            Assert.Equal(existingProduct.Category_Id, resultDelete.Category_Id);

            Assert.Equal(2, getResult.Count());
        }
    }
}
