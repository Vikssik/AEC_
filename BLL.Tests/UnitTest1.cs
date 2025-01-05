using Moq;
using BLL.Services.Interfaces;
using BLL.Services.Impl;
using Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces;

namespace BLL.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CheckOperationAccess_UserHasAccess_ReturnsTrue()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockOperationRepository = new Mock<IOperationRepository>();
            mockUnitOfWork.Setup(uow => uow.Operations).Returns(mockOperationRepository.Object);

            var userId = 1;
            var operationName = "ServiceSensors";

            // Setup repository to return an operation
            mockOperationRepository.Setup(r => r.Find(It.IsAny<Func<Operation, bool>>()))
                .Returns(new List<Operation> { new Operation { OperationName = "ServiceSensors" } });

            IOperationService operationService = new OperationService(mockUnitOfWork.Object);

            // Act
            var result = operationService.CheckOperationAccess(userId, operationName);

            // Assert
            Assert.True(result);
        }
    }
}
