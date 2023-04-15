using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CqrsFramework.Application.Features.Category.Commands;
using CqrsFramework.Application.Features.Category.Models;
using CqrsFramework.Application.Features.Category.Queries;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TestProject1.Features
{
    [Collection("TestCollection")]
    public class CategoryTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CategoryTests()
        {
            _mapper = MapperBuilder.CategoryMapper();
            //_context = TestDatabaseContext.Instance.Context;
            _context = new DynamicDatabaseBuilder().CreateDatabaseContext();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task Create_Category()
        {
            // Arrange
            var newCategory = new CreateCategoryCommand
            {
                Name = "New Category"
            };

            var handler = new CreateCategoryCommandHandler(_mapper, _context);

            var getQuery = new GetCategoryQuery();
            var getHandler = new GetCategoryQueryHandler(_mapper, _context);

            //Act
            var result = await handler.Handle(newCategory, CancellationToken.None);
            var getResult = await getHandler.Handle(getQuery, CancellationToken.None);
            var newAddedCategory = _context.Categories.Find(4);
            // Assert

            //Assert.Null(result);

            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
            Assert.Equal(newAddedCategory.Name, result.Name);
            Assert.Equal(newAddedCategory.Id, result.Id);

            Assert.Equal(4, getResult.Count());
        }

        [Fact]
        public async Task Update_Category()
        {
            // Arrange
            var category = new CategoryEntity
            {
                Id = 4,
                Name = "Test Category"
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var command = new UpdateCategoryCommand
            {
                Id = 4,
                Name = "Updated Category"
            };

            var existingCategory = _context.Categories.Find(category.Id);
            if (existingCategory != null)
            {
                _context.Entry(existingCategory).State = EntityState.Detached;
            }

            var handler = new UpdateCategoryCommandHandler(_mapper, _context);


            var getQuery = new GetCategoryQuery();
            var getHandler = new GetCategoryQueryHandler(_mapper, _context);


            //Act
            var result = await handler.Handle(command, CancellationToken.None);
            var getResult = await getHandler.Handle(getQuery, CancellationToken.None);

            // Assert

            //Assert.Null(result);

            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Id, result.Id);

            Assert.Equal(4, getResult.Count());
        }

        [Fact]
        public async Task Get_Category()
        {
            // Arrange
            var query = new GetCategoryQuery();
            var handler = new GetCategoryQueryHandler(_mapper, _context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task Delete_Category()
        {
            // Arrange
            DeleteCategoryCommand command = new DeleteCategoryCommand(1);

            var existingCategory = _context.Categories.Find(command.Id);

            var handler = new DeleteCategoryCommandHandler(_mapper, _context);


            var getQuery = new GetCategoryQuery();
            var getHandler = new GetCategoryQueryHandler(_mapper, _context);


            //Act
            var resultDelete = await handler.Handle(command, CancellationToken.None);
            var getResult = await getHandler.Handle(getQuery, CancellationToken.None);

            // Assert

            //Assert.Null(result);

            Assert.NotNull(resultDelete);
            Assert.IsType<CategoryResponse>(resultDelete);
            Assert.Equal(existingCategory.Name, resultDelete.Name);
            Assert.Equal(existingCategory.Id, resultDelete.Id);

            Assert.Equal(2, getResult.Count());
        }
    }
}
