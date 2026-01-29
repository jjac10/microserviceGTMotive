using System;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain.Entities
{
    /// <summary>
    /// Unit test for Vehicle entity.
    /// </summary>
    public class VehicleTests
    {
        /// <summary>
        /// Tests that creating a vehicle with valid data succeeds.
        /// </summary>
        [Fact]
        public void CreateVehicleWithValidDataShouldSucceed()
        {
            // Arrange
            var brand = "Toyota";
            var model = "Corolla";
            var licensePlate = "1234ABC";
            var manufacturingDate = DateTime.UtcNow.AddYears(-3);

            // Act
            var vehicle = new Vehicle(brand, model, licensePlate, manufacturingDate);

            // Assert
            Assert.Equal(brand, vehicle.Brand);
            Assert.Equal(model, vehicle.Model);
            Assert.Equal(licensePlate, vehicle.LicensePlate);
            Assert.Equal(manufacturingDate, vehicle.ManufacturingDate);
            Assert.True(vehicle.IsAvailable);
        }

        /// <summary>
        /// Tests that creating a vehicle with an incorrect license plate format throws a DomainException.
        /// </summary>
        [Fact]
        public void CreateVehicleWithIncorrectLicensePlateShouldThrowDomainException()
        {
            // Arrange
            var brand = "Honda";
            var model = "Civic";
            var licensePlate = "12AB34"; // Invalid format
            var manufacturingDate = DateTime.UtcNow.AddYears(-2);

            // Act & Assert
            var exception = Assert.Throws<InvalidLicensePlateException>(() =>
                new Vehicle(brand, model, licensePlate, manufacturingDate));
            Assert.Equal($"The license plate '{licensePlate}' is invalid. Expected format: 1234ABC", exception.Message);
        }

        /// <summary>
        /// Tests that creating a vehicle older than 5 years throws a VehicleTooOldException.
        /// </summary>
        [Fact]
        public void CreateVehicleOlderThan5YearsShouldThrowVehicleTooOldException()
        {
            // Arrange
            var brand = "Ford";
            var model = "Focus";
            var licensePlate = "5678DEF";
            var manufacturingDate = DateTime.UtcNow.AddYears(-6);

            // Act & Assert
            var exception = Assert.Throws<VehicleTooOldException>(() =>
                new Vehicle(brand, model, licensePlate, manufacturingDate));
            Assert.Equal("The vehicle is over 5 years old and cannot be registered.", exception.Message);
        }

        /// <summary>
        /// Tests that marking a vehicle as rented changes its availability.
        /// </summary>
        [Fact]
        public void MarkAsRendedShouldSetIsAvailableToFalse()
        {
            // Arrange
            var vehicle = new Vehicle("BMW", "X3", "9012GHI", DateTime.UtcNow.AddYears(-1));

            // Act
            vehicle.MarkAsRented();

            // Assert
            Assert.False(vehicle.IsAvailable);
        }

        /// <summary>
        /// Tests that marking a vehicle as available changes its availability.
        /// </summary>
        [Fact]
        public void MarkAsAvailableShouldSetIsAvailableToTrue()
        {
            // Arrange
            var vehicle = new Vehicle("BMW", "X3", "9012GHI", DateTime.UtcNow.AddYears(-1));

            // Act
            vehicle.MarkAsRented();
            vehicle.MarkAsAvailable();

            // Assert
            Assert.True(vehicle.IsAvailable);
        }
    }
}
