using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Rental entity.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Gets an active rental by customerId.
        /// </summary>
        /// <param name="customerId">Customer Identifier.</param>
        /// <returns>The active rental for the specified customer, null otherwise.</returns>
        Task<Rental> GetActiveByCustomerIdAsync(Guid customerId);

        /// <summary>
        /// Gets an active rental by vehicle identifier.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <returns>The active rental if found, null otherwise.</returns>
        Task<Rental> GetActiveByVehicleIdAsync(Guid vehicleId);

        /// <summary>
        /// Adds a new rental to the repository.
        /// </summary>
        /// <param name="rental">The rental to add.</param>
        /// <returns>A task representing the added rental.</returns>
        Task AddAsync(Rental rental);

        /// <summary>
        /// Updates an existing rental.
        /// </summary>
        /// <param name="rental">The rental to update.</param>
        /// <returns>A task that representing the updated rental.</returns>
        Task UpdateAsync(Rental rental);
    }
}
