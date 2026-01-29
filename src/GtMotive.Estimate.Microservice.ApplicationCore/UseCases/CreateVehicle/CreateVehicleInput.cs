using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Input for creating a new vehicle.
    /// </summary>
    /// <param name="Brand">Vehicle brand.</param>
    /// <param name="Model">Vehicle model.</param>
    /// <param name="LicensePlate">Vehicle license plate.</param>
    /// <param name="ManufacturingDate">Vehicle manufacturing date.</param>
    public record CreateVehicleInput(string Brand, string Model, string LicensePlate, DateTime ManufacturingDate)
        : IUseCaseInput;
}
