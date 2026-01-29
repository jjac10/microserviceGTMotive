using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output for renting a vehicle.
    /// </summary>
    /// <param name="Id">Rental identifier.</param>
    /// <param name="VehicleId">Vehicle identifier.</param>
    /// <param name="CustomerId">Customer identifier.</param>
    /// <param name="StartDate">Rental start date.</param>
    public record RentVehicleOutput(Guid Id, Guid VehicleId, Guid CustomerId, DateTime StartDate) : IUseCaseOutput;
}
