using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.UseCases
{
    /// <summary>
    /// Unit tests for ReturnVehicleUseCase.
    /// </summary>
    public class ReturnVehicleUseCaseTests
    {
        private readonly Mock<IRentalRepository> _mockRentalRepo;
        private readonly Mock<IVehicleRepository> _mockVehicleRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IReturnVehicleOutputPort> _mockOutputPort;
        private readonly Mock<IAppLogger<ReturnVehicleUseCase>> _mockLogger;

        private readonly ReturnVehicleUseCase _useCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleUseCaseTests"/> class.
        /// </summary>
        public ReturnVehicleUseCaseTests()
        {
            _mockRentalRepo = new Mock<IRentalRepository>();
            _mockVehicleRepo = new Mock<IVehicleRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockOutputPort = new Mock<IReturnVehicleOutputPort>();
            _mockLogger = new Mock<IAppLogger<ReturnVehicleUseCase>>();
            _useCase = new ReturnVehicleUseCase(
                _mockVehicleRepo.Object,
                _mockRentalRepo.Object,
                _mockUnitOfWork.Object,
                _mockOutputPort.Object,
                _mockLogger.Object);
        }

        /// <summary>
        /// Tests that return a vehicle.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task ReturnVehicleSuccessfully()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var vehicle = new Vehicle("Ford", "Kuga", "1111AAA", DateTime.Now);
            vehicle.MarkAsRented();

            var rental = new Rental(vehicle.Id, customerId, DateTime.UtcNow.AddDays(-2));

            var input = new ReturnVehicleInput(vehicle.Id);

            _mockRentalRepo.Setup(r => r.GetActiveByVehicleIdAsync(vehicle.Id)).ReturnsAsync(rental);
            _mockVehicleRepo.Setup(v => v.GetByIdAsync(vehicle.Id)).ReturnsAsync(vehicle);

            // Act
            await _useCase.Execute(input);

            // Assert
            _mockVehicleRepo.Verify(v => v.UpdateAsync(It.Is<Vehicle>(veh => vehicle.IsAvailable)), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
            _mockOutputPort.Verify(p => p.StandardHandle(It.IsAny<ReturnVehicleOutput>()), Times.Once);
        }
    }
}
