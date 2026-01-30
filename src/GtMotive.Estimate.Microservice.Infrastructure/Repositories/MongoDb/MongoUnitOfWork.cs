using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories.MongoDb
{
    /// <summary>
    /// MongoDb implementation of the Unit of Work.
    /// </summary>
    /// <remarks>
    /// Since MongoDB repositories typically persist changes immediately upon operation,
    /// this method serves as a no-op (no operation) to satisfy the <see cref="IUnitOfWork"/> interface contract.
    /// </remarks>
    public class MongoUnitOfWork : IUnitOfWork
    {
        public Task<int> Save()
        {
            // No pending changes to commit.
            return Task.FromResult(0);
        }
    }
}
