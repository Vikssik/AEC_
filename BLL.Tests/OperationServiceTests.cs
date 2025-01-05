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
	public class OperationServiceTests
	{
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly OperationService _operationService;

		public OperationServiceTests()
		{
			_unitOfWorkMock = new Mock<IUnitOfWork>();

			_operationService = new OperationService(_unitOfWorkMock.Object);
		}

		[Fact]
		public void CheckOperationAccess_TechnicalSpecialist_AllowedOperation_ReturnsTrue()
		{
			// Arrange
			var userId = 1;
			var operationName = "ServiceSensors";
			var user = new TechnicalSpecialist(1, "ServiceSensors");
			SecurityContext.SetCurrentUser(user);
			
			// Act
			var result = _operationService.CheckOperationAccess(operationName);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void CheckOperationAccess_TechnicalSpecialist_DisallowedOperation_ReturnsFalse()
		{
			// Arrange
			var userId = 1;
			var operationName = "CreateUser";
			var user = new TechnicalSpecialist(1, "ServiceSensors");
			SecurityContext.SetCurrentUser(user);

			// Act
			var result = _operationService.CheckOperationAccess(operationName);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void GetOperationsByRole_ValidRole_ReturnsOperations()
		{
			// Arrange
			var roleName = nameof(SystemAdministrator);
			var operations = new List<Operation>
			{
				new Operation { OperationName = "CreateUser" },
				new Operation { OperationName = "ReadUser" }
			};

			_unitOfWorkMock.Setup(u => u.Operations.Find(It.IsAny<Func<Operation, bool>>()))
				.Returns(operations.AsQueryable());

			// Act
			var result = _operationService.GetOperationsByRole(roleName);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count());
			Assert.Contains(result, o => o.OperationName == "CreateUser");
			Assert.Contains(result, o => o.OperationName == "ReadUser");
		}

		[Fact]
		public void GetOperationsByRole_InvalidRole_ThrowsException()
		{
			// Arrange
			var roleName = "InvalidRole";

			// Act & Assert
			Assert.Throws<Exception>(() => _operationService.GetOperationsByRole(roleName));
		}
	}
}
