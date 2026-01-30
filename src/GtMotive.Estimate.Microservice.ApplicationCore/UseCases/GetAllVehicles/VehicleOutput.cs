using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAllVehicles
{
    /// <summary>
    /// Output representation of a vehicle.
    /// </summary>
    /// <param name="Id">Vehicle id.</param>
    /// <param name="Brand">Vehicle brand.</param>
    /// <param name="Model">Vehicle model.</param>
    /// <param name="LicensePlate">Vehicle license plate.</param>
    /// <param name="ManufacturingDate">Vehicle manufacturing date.</param>
    /// <param name="IsAvailable">Vehicle is available.</param>
    public record VehicleOutput(Guid Id, string Brand, string Model, string LicensePlate, DateTime ManufacturingDate, bool IsAvailable);
}
