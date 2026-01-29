using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories.InMemory
{
    /// <summary>
    /// In Memory implementation of the Unit of Work.
    /// </summary>
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public Task<int> Save()
        {
            return Task.FromResult(0);
        }
    }
}
