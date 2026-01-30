using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories.MongoDb
{
    public class MongoVehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _collection;

        public MongoVehicleRepository(MongoService mongoService, IOptions<MongoDbSettings> options)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            ArgumentNullException.ThrowIfNull(options);

            var database = mongoService.MongoClient.GetDatabase(options.Value.MongoDbDatabaseName);
            _collection = database.GetCollection<Vehicle>("vehicles");
        }

        public async Task<Vehicle> GetByIdAsync(Guid id)
        {
            return await _collection.Find(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAvailablesAsync()
        {
            return await _collection.Find(v => v.IsAvailable).ToListAsync();
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _collection.InsertOneAsync(vehicle);
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            await _collection.ReplaceOneAsync(v => v.Id == vehicle.Id, vehicle);
        }

        public async Task<bool> ExistsByLicensePlateAsync(string licensePlate)
        {
            var count = await _collection.CountDocumentsAsync(v => v.LicensePlate == licensePlate);
            return count > 0;
        }
    }
}
