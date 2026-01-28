using System;
using System.Text.RegularExpressions;

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
        /// <param name="id">The unique identifier for the vehicle.</param>
        /// <param name="brand">The manufacturer or brand name of the vehicle. Cannot be null or empty.</param>
        /// <param name="model">The model designation of the vehicle. Cannot be null or empty.</param>
        /// <param name="licensePlate">The license plate number assigned to the vehicle. Cannot be null or empty.</param>
        /// <param name="manufacturingDate">The date the vehicle was manufactured.</param>
        public Vehicle(Guid id, string brand, string model, string licensePlate, DateTime manufacturingDate)
        {
            if (manufacturingDate < DateTime.UtcNow.AddYears(-5))
            {
                throw new DomainException("The vehicle is over 5 years old and cannot be registered.");
            }

            if (string.IsNullOrWhiteSpace(licensePlate) || !LicensePlateRegex().IsMatch(licensePlate))
            {
                throw new DomainException("The license plate format is invalid. Expected format: 1234ABC");
            }

            Id = id;
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
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the brand name associated with the item.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the name or identifier of the model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the numeric license plate identifier for the vehicle.
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets the date when the item was manufactured.
        /// </summary>
        public DateTime ManufacturingDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the resource is currently available.
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Checks if the license plate matches the spanish format: 4 digits followed by 3 uppercase letters (e.g., 1234ABC).
        /// </summary>
        [GeneratedRegex(@"^\d{4}[A-Z]{3}$", RegexOptions.Compiled)]
        private static partial Regex LicensePlateRegex();
    }
}
