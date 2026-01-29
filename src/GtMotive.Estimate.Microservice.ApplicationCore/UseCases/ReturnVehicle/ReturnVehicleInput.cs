using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Input for return a vehicle use case.
    /// </summary>
    /// <param name="VehicleId">The vehicle identifier.</param>
    public record ReturnVehicleInput(Guid VehicleId) : IUseCaseInput;
}
