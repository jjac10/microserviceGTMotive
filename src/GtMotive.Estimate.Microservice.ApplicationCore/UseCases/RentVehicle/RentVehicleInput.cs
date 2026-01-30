using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Input for renting a vehicle.
    /// </summary>
    /// <param name="VehicleId">Vehicle identifier.</param>
    /// <param name="CustomerId">Customer identifier.</param>
    public record RentVehicleInput(Guid VehicleId, Guid CustomerId) : IUseCaseInput;
}
