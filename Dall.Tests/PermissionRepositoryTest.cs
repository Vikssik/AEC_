using Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Dall.Tests
{
    public class PermissionRepositoryTests
    {
        [Fact]
        public void Create_AddsPermissionToContext()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Permission>>();
            var mockContext = new Mock<PermissionContext>(new DbContextOptions<PermissionContext>());
            var newPermission = new Permission { PermissionID = 1, PermissionName = "View", Role = "Admin" };

            
            mockDbSet.Setup(m => m.Add(It.IsAny<Permission>())).Callback<Permission>(p => { });

            mockContext.Setup(c => c.Set<Permission>()).Returns(mockDbSet.Object);
            var repository = new PermissionRepository(mockContext.Object);

            // Act
            repository.Create(newPermission);

            // Assert
            mockDbSet.Verify(m => m.Add(It.IsAny<Permission>()), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
        [Fact]
        public void Update_UpdatesPermissionRole()
        {
            // Arrange
            var permission = new Permission { PermissionID = 1, PermissionName = "View", Role = "Admin" };

            
            var data = new List<Permission> { permission }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Permission>>();

           
            mockDbSet.As<IQueryable<Permission>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Permission>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Permission>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Permission>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            
            mockDbSet.Setup(m => m.Find(It.IsAny<int>())).Returns<int>(id => data.FirstOrDefault(p => p.PermissionID == id));

            
            var mockContext = new Mock<PermissionContext>(new DbContextOptions<PermissionContext>());
            mockContext.Setup(c => c.Permissions).Returns(mockDbSet.Object);

            
            var repository = new PermissionRepository(mockContext.Object);

            // Act
            repository.UpdateRole(permission.PermissionID, "User");

            // Assert
            Assert.Equal("User", permission.Role); 

            
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
        
    }

 }


