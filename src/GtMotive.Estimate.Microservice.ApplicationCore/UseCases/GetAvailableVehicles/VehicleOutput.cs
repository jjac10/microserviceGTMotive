using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Output representation of a vehicle.
    /// </summary>
    /// <param name="id">Vehicle id.</param>
    /// <param name="brand">Vehicle brand.</param>
    /// <param name="model">Vehicle model.</param>
    /// <param name="licensePlate">Vehicle license plate.</param>
    /// <param name="manufacturingDate">Vehicle manufacturing date.</param>
    public class VehicleOutput(Guid id, string brand, string model, string licensePlate, DateTime manufacturingDate)
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid Id { get; } = id;

        /// <summary>
        /// Gets the vehicle brand.
        /// </summary>
        public string Brand { get; } = brand;

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        public string Model { get; } = model;

        /// <summary>
        /// Gets the vehicle license plate.
        /// </summary>
        public string LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the vehicle manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; } = manufacturingDate;
    }
}
