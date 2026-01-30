using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories.InMemory
{
    /// <summary>
    /// In Memory implementation of the Unit of Work.
    /// </summary>
    /// <remarks>
    /// For in-memory repositories, this is a no-op since changes are applied immediately.
    /// </remarks>
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public Task<int> Save()
        {
            // In-memory repositories apply changes immediately,
            // so there's nothing to save here.
            return Task.FromResult(0);
        }
    }
}
