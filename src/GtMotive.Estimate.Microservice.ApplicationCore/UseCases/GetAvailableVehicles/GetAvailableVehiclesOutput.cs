using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Output for getting available vehicles.
    /// </summary>
    /// <param name="Vehicles">Available vehicles.</param>
    public record GetAvailableVehiclesOutput(IEnumerable<VehicleOutput> Vehicles) : IUseCaseOutput;
}
