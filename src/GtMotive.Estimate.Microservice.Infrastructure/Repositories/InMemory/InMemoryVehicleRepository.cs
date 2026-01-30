using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories.InMemory
{
    /// <summary>
    /// In Memory implementation of the Vehicle repository.
    /// </summary>
    public class InMemoryVehicleRepository : IVehicleRepository
    {
        private static readonly ConcurrentDictionary<Guid, Vehicle> Vehicles = new();

        public Task AddAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            Vehicles.TryAdd(vehicle.Id, vehicle);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsByLicensePlateAsync(string licensePlate)
        {
            var exists = Vehicles.Values.Any(x => x.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(exists);
        }

        public Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            var vehicles = Vehicles.Values.ToList();
            return Task.FromResult<IEnumerable<Vehicle>>(vehicles);
        }

        public Task<IEnumerable<Vehicle>> GetAvailablesAsync()
        {
            var availables = Vehicles.Values.Where(x => x.IsAvailable);
            return Task.FromResult(availables);
        }

        public Task<Vehicle> GetByIdAsync(Guid id)
        {
            Vehicles.TryGetValue(id, out var vehicle);
            return Task.FromResult(vehicle);
        }

        public Task UpdateAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            Vehicles[vehicle.Id] = vehicle;
            return Task.CompletedTask;
        }
    }
}
