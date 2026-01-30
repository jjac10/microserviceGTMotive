using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories.MongoDb
{
    public class MongoRentalRepository : IRentalRepository
    {
        private readonly IMongoCollection<Rental> _collection;

        public MongoRentalRepository(MongoService mongoService, IOptions<MongoDbSettings> options)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            ArgumentNullException.ThrowIfNull(options);

            var database = mongoService.MongoClient.GetDatabase(options.Value.MongoDbDatabaseName);
            _collection = database.GetCollection<Rental>("rentals");
        }

        public async Task<Rental> GetActiveByCustomerIdAsync(Guid customerId)
        {
            return await _collection.Find(r => r.CustomerId == customerId && r.IsActive).FirstOrDefaultAsync();
        }

        public async Task<Rental> GetActiveByVehicleIdAsync(Guid vehicleId)
        {
            return await _collection.Find(r => r.VehicleId == vehicleId && r.IsActive).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Rental rental)
        {
            await _collection.InsertOneAsync(rental);
        }

        public async Task UpdateAsync(Rental rental)
        {
            await _collection.ReplaceOneAsync(r => r.Id == rental.Id, rental);
        }
    }
}
