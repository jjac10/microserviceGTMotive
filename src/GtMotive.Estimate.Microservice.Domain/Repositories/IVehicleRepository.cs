using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Vehicle entitiy.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Gets a vehicle by its identifier.
        /// </summary>
        /// <param name="id">Vehicle identifier.</param>
        /// <returns>The vehicle if found, null otherwise.</returns>
        Task<Vehicle> GetByIdAsync(Guid id);

        /// <summary>
        /// Get all available vehicles.
        /// </summary>
        /// <returns>A list of available vehicles.</returns>
        Task<IEnumerable<Vehicle>> GetAvailablesAsync();

        /// <summary>
        /// Add a new vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task that representing the added rental.</returns>
        Task AddAsync(Vehicle vehicle);

        /// <summary>
        /// Update an existing vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle to update.</param>
        /// <returns>A task that representing the updated rental.</returns>
        Task UpdateAsync(Vehicle vehicle);

        /// <summary>
        /// Checks if a vehicle exists by its license plate.
        /// </summary>
        /// <param name="licensePlate">The license plate to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        Task<bool> ExistsByLicensePlateAsync(string licensePlate);
    }
}
