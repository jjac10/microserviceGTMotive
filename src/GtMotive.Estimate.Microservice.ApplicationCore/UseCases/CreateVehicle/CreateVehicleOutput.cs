using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Output for creating a new vehicle.
    /// </summary>
    /// <param name="Id">Vehicle id.</param>
    /// <param name="Brand">Vehicle brand.</param>
    /// <param name="Model">Vehicle model.</param>
    /// <param name="LicensePlate">Vehicle license plate.</param>
    /// <param name="ManufacturingDate">Vehicle manufacturing date.</param>
    public record CreateVehicleOutput(Guid Id, string Brand, string Model, string LicensePlate, DateTime ManufacturingDate)
        : IUseCaseOutput;
}
