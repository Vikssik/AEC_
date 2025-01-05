using AutoMapper;
using BLL.DTO;
using BLL.Services.Impl;
using Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces;
using CLL.Security.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Tests
{
	public class PermissionServiceTests
	{
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly IMapper _mapper;
		private readonly PermissionService _permissionService;

		public PermissionServiceTests()
		{
			_unitOfWorkMock = new Mock<IUnitOfWork>();

			// Configure AutoMapper
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Permission, PermissionDTO>();
			});
			_mapper = config.CreateMapper();

			_permissionService = new PermissionService(_unitOfWorkMock.Object);
		}

		[Fact]
		public void CheckPermission_TechnicalSpecialist_AllowedPermission_ReturnsTrue()
		{
			// Arrange
			var userId = 1;
			var permissionName = "ServiceSensors";
			var user = new TechnicalSpecialist(userId, permissionName);
			SecurityContext.SetCurrentUser(user);

			// Act
			var result = _permissionService.CheckPermission(permissionName);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void CheckPermission_TechnicalSpecialist_DisallowedPermission_ReturnsFalse()
		{
			// Arrange
			var userId = 1;
			var permissionName = "CreateUser";
			var user = new TechnicalSpecialist(userId, permissionName);
			SecurityContext.SetCurrentUser(user);

			// Act
			var result = _permissionService.CheckPermission(permissionName);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void GetPermissionsByRole_ValidRole_ReturnsPermissions()
		{
			// Arrange
			var roleName = nameof(SystemAdministrator);
			var permissions = new List<Permission>
			{
				new Permission { PermissionName = "CreateUser" },
				new Permission { PermissionName = "ReadUser" }
			};

			_unitOfWorkMock.Setup(u => u.Permissions.Find(It.IsAny<Func<Permission, bool>>()))
				.Returns(permissions.AsQueryable());

			// Act
			var result = _permissionService.GetPermissionsByRole(roleName);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count());
			Assert.Contains(result, p => p.PermissionName == "CreateUser");
			Assert.Contains(result, p => p.PermissionName == "ReadUser");
		}

		[Fact]
		public void GetPermissionsByRole_InvalidRole_ThrowsException()
		{
			// Arrange
			var roleName = "InvalidRole";

			// Act & Assert
			Assert.Throws<Exception>(() => _permissionService.GetPermissionsByRole(roleName));
		}
	}
}
