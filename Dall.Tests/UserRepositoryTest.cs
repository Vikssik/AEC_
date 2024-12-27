using Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

    namespace Dall.Tests
    {
        public class UserRepositoryUnitTests
        {
            [Fact]
            public void Get_ReturnsUser_WhenUserExists()
            {
                // Arrange
                var mockDbSet = new Mock<DbSet<User>>();
                var mockContext = new Mock<UserContext>(new DbContextOptions<UserContext>());
                var user = new User { UserID = 1, Username = "testuser", Email = "test@example.com" };
                mockDbSet.Setup(m => m.Find(1)).Returns(user);
                mockContext.Setup(c => c.Set<User>()).Returns(mockDbSet.Object);

                var repository = new UserRepository(mockContext.Object);

                // Act
                var result = repository.Get(1);

                // Assert
                Assert.Equal(user, result);
            }

            [Fact]
            public void Create_AddsUser_AndSavesChanges()
            {
                // Arrange
                var mockDbSet = new Mock<DbSet<User>>();
                var mockContext = new Mock<UserContext>(new DbContextOptions<UserContext>());
                var user = new User { UserID = 1, Username = "testuser", Email = "test@example.com" };
                mockContext.Setup(c => c.Set<User>()).Returns(mockDbSet.Object);

                var repository = new UserRepository(mockContext.Object);

                // Act
                repository.Create(user);

                // Assert
                mockDbSet.Verify(dbSet => dbSet.Add(It.Is<User>(u => u.UserID == user.UserID)), Times.Once());
                mockContext.Verify(c => c.SaveChanges(), Times.Once());
            }
        }
    }

