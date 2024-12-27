using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces.Tests
{
    public class OperationRepositoryTests
    {
        private readonly OperationRepository _repository;
        private readonly Mock<DbSet<Operation>> _mockDbSet;
        private readonly Mock<ApplicationDbContext> _mockContext;

        public OperationRepositoryTests()
        {
            _mockDbSet = new Mock<DbSet<Operation>>();
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _mockContext.Setup(c => c.Set<Operation>()).Returns(_mockDbSet.Object);
            _repository = new OperationRepository(_mockContext.Object);
        }

        [Fact]
        public void Create_AddsOperation_AndSavesChanges()
        {
            // Arrange
            var operation = new Operation { OperationID = 1, OperationName = "TestOperation", CreatedAt = DateTime.Now };

            // Act
            _repository.Create(operation);

            // Assert
            _mockDbSet.Verify(dbSet => dbSet.Add(It.Is<Operation>(o => o == operation)), Times.Once());
            _mockContext.Verify(context => context.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Update_UpdatesOperation_AndSavesChanges()
        {
            // Arrange
            var operation = new Operation { OperationID = 1, OperationName = "TestOperation", CreatedAt = DateTime.Now };

            // Act
            _repository.Update(operation);

            // Assert
            _mockDbSet.Verify(dbSet => dbSet.Update(It.Is<Operation>(o => o == operation)), Times.Once());
            _mockContext.Verify(context => context.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Delete_RemovesOperation_AndSavesChanges()
        {
            // Arrange
            var operation = new Operation { OperationID = 1, OperationName = "TestOperation", CreatedAt = DateTime.Now };
            _mockDbSet.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(operation);

            // Act
            _repository.Delete(operation.OperationID);

            // Assert
            _mockDbSet.Verify(dbSet => dbSet.Remove(It.Is<Operation>(o => o == operation)), Times.Once());
            _mockContext.Verify(context => context.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Get_ReturnsOperation_WhenOperationExists()
        {
            // Arrange
            var operation = new Operation { OperationID = 1, OperationName = "TestOperation", CreatedAt = DateTime.Now };
            _mockDbSet.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(operation);

            // Act
            var result = _repository.Get(1);

            // Assert
            Assert.Equal(operation, result);
        }

    }
}