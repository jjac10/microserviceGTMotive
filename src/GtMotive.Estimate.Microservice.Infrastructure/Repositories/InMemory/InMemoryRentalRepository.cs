using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories.InMemory
{
    /// <summary>
    /// In Memory implementation of the Rental repository.
    /// </summary>
    public class InMemoryRentalRepository : IRentalRepository
    {
        private static readonly ConcurrentDictionary<Guid, Rental> Rentals = new();

        public Task AddAsync(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            Rentals.TryAdd(rental.Id, rental);
            return Task.CompletedTask;
        }

        public Task<Rental> GetActiveByCustomerIdAsync(Guid customerId)
        {
            var rental = Rentals.Values.FirstOrDefault(x => x.CustomerId == customerId && x.IsActive);
            return Task.FromResult(rental);
        }

        public Task<Rental> GetActiveByVehicleIdAsync(Guid vehicleId)
        {
            var rental = Rentals.Values.FirstOrDefault(x => x.VehicleId == vehicleId && x.IsActive);
            return Task.FromResult(rental);
        }

        public Task UpdateAsync(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            Rentals[rental.Id] = rental;
            return Task.CompletedTask;
        }
    }
}
