using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repositories;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.UseCases
{
    /// <summary>
    /// Functional tests for CreateVehicleUseCase.
    /// </summary>
    /// <param name="fixture">The fixture that provides the service provider and configuration for the tests.</param>
    [Collection(TestCollections.Functional)]
    public class CreateVehicleInMemoryTests(CompositionRootTestFixture fixture) : IClassFixture<CompositionRootTestFixture>
    {
        private readonly CompositionRootTestFixture _fixture = fixture;

        [Fact]
        public async Task CreateVehicleWithValidDataInMemory()
        {
            // Arrange
            var licensePlate = "1234ABC";
            var input = new Vehicle("Renault", "Clio", licensePlate, DateTime.UtcNow.AddYears(-1));

            // Act & Assert
            await _fixture.UsingRepository<IVehicleRepository>(async repository =>
            {
                await repository.AddAsync(input);

                var vehicles = await repository.GetAvailablesAsync();

                Assert.Contains(vehicles, v => v.LicensePlate == licensePlate);
            });
        }
    }
}
