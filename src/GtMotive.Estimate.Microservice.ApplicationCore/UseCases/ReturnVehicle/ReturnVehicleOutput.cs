using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output for return a vehicle use case.
    /// </summary>
    /// <param name="RentalId">Rental identifier.</param>
    /// <param name="VehicleId">Vehicle identifier.</param>
    /// <param name="CustomerId">Customer identifier.</param>
    /// <param name="StartDate">Rental start date.</param>
    /// <param name="EndDate">Rental end date.</param>
    public record ReturnVehicleOutput(Guid RentalId, Guid VehicleId, Guid CustomerId, DateTime StartDate, DateTime EndDate) : IUseCaseOutput;
}
