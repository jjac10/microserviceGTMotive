using System;
using System.Text.RegularExpressions;
using GtMotive.Estimate.Microservice.Domain.Exceptions;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Vehicle entity representing a vehicle in the rental company.
    /// </summary>
    public partial class Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="brand">The manufacturer or brand name of the vehicle. Cannot be null or empty.</param>
        /// <param name="model">The model designation of the vehicle. Cannot be null or empty.</param>
        /// <param name="licensePlate">The license plate number assigned to the vehicle. Cannot be null or empty.</param>
        /// <param name="manufacturingDate">The date the vehicle was manufactured.</param>
        public Vehicle(string brand, string model, string licensePlate, DateTime manufacturingDate)
        {
            if (manufacturingDate < DateTime.UtcNow.AddYears(-5))
            {
                throw new VehicleTooOldException();
            }

            if (string.IsNullOrWhiteSpace(licensePlate) || !LicensePlateRegex().IsMatch(licensePlate))
            {
                throw new InvalidLicensePlateException(licensePlate);
            }

            Id = Guid.NewGuid();
            Brand = brand;
            Model = model;
            LicensePlate = licensePlate;
            ManufacturingDate = manufacturingDate;
            IsAvailable = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        public Vehicle()
        {
        }

        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the brand name associated with the item.
        /// </summary>
        public string Brand { get; private set; }

        /// <summary>
        /// Gets the name or identifier of the model.
        /// </summary>
        public string Model { get; private set; }

        /// <summary>
        /// Gets the numeric license plate identifier for the vehicle.
        /// </summary>
        public string LicensePlate { get; private set; }

        /// <summary>
        /// Gets the date when the item was manufactured.
        /// </summary>
        public DateTime ManufacturingDate { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the vehicle is currently available.
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Marks the vehicle as rented.
        /// </summary>
        public void MarkAsRented()
        {
            IsAvailable = false;
        }

        /// <summary>
        /// Marks the vehicle as available.
        /// </summary>
        public void MarkAsAvailable()
        {
            IsAvailable = true;
        }

        /// <summary>
        /// Checks if the license plate matches the spanish format: 4 digits followed by 3 uppercase letters (e.g., 1234ABC).
        /// </summary>
        [GeneratedRegex(@"^\d{4}[A-Z]{3}$", RegexOptions.Compiled)]
        private static partial Regex LicensePlateRegex();
    }
}
