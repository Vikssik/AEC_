using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces.Tests
{
    public class BaseRepositoryTests
    {
        public class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private readonly Mock<DbSet<TestEntity>> _mockDbSet;
        private readonly Mock<DbContext> _mockContext;
        private readonly BaseRepository<TestEntity> _repository;

        public BaseRepositoryTests()
        {
            _mockDbSet = new Mock<DbSet<TestEntity>>();
            _mockContext = new Mock<DbContext>();
            _mockContext.Setup(context => context.Set<TestEntity>()).Returns(_mockDbSet.Object);
            _repository = new BaseRepository<TestEntity>(_mockContext.Object);
        }

        [Fact]
        public void Create_AddsEntity_AndSavesChanges()
        {
            // Arrange
            var entity = new TestEntity { Id = 1, Name = "Test" };

            // Act
            _repository.Create(entity);

            // Assert
            _mockDbSet.Verify(dbSet => dbSet.Add(It.Is<TestEntity>(e => e == entity)), Times.Once());
            _mockContext.Verify(context => context.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Update_UpdatesEntity_AndSavesChanges()
        {
            // Arrange
            var entity = new TestEntity { Id = 1, Name = "Test" };

            // Act
            _repository.Update(entity);

            // Assert
            _mockDbSet.Verify(dbSet => dbSet.Update(It.Is<TestEntity>(e => e == entity)), Times.Once());
            _mockContext.Verify(context => context.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Delete_RemovesEntity_AndSavesChanges()
        {
            // Arrange
            var entity = new TestEntity { Id = 1, Name = "Test" };
            _mockDbSet.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(entity);

            // Act
            _repository.Delete(entity.Id);

            // Assert
            _mockDbSet.Verify(dbSet => dbSet.Remove(It.Is<TestEntity>(e => e == entity)), Times.Once());
            _mockContext.Verify(context => context.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Get_ReturnsEntity_WhenEntityExists()
        {
            // Arrange
            var entity = new TestEntity { Id = 1, Name = "Test" };
            _mockDbSet.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(entity);

            // Act
            var result = _repository.Get(1);

            // Assert
            Assert.Equal(entity, result);
        }

    }
}
