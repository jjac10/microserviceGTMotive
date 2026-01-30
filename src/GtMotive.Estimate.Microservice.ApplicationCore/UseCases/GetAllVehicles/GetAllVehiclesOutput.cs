using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAllVehicles
{
    /// <summary>
    /// Output for getting all vehicles.
    /// </summary>
    /// <param name="Vehicles">All vehicles.</param>
    public record GetAllVehiclesOutput(IEnumerable<VehicleOutput> Vehicles) : IUseCaseOutput;
}
