using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Repositories;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Api
{
    /// <summary>
    /// Integration tests for Create Vehicle API endpoint.
    /// </summary>
    /// <param name="fixture">The fixture that provides the test server and service configuration.</param>
    [Collection(TestCollections.TestServer)]
    public class CreateVehicleApiTests(GenericInfrastructureTestServerFixture fixture) : InfrastructureTestBase(fixture)
    {
        [Fact]
        public async Task PostShouldCreateVehicleSuccesfully()
        {
            // Arrange
            var licensePlate = "9999ZZZ";
            var client = Fixture.Server.CreateClient();
            var request = new
            {
                Brand = "Ford",
                Model = "Focus",
                LicensePlate = licensePlate,
                ManufacturingDate = DateTime.UtcNow.AddYears(-2)
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/vehicles", request);

            // Assert
            response.EnsureSuccessStatusCode();

            var repository = Fixture.Server.Services.GetRequiredService<IVehicleRepository>();
            var vehicles = await repository.GetAvailablesAsync();
            Assert.Contains(vehicles, v => v.LicensePlate == licensePlate);
        }
    }
}
