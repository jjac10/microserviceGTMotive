using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    /// <summary>
    /// feaf.
    /// </summary>
    public class FakeUnitOfWork : IUnitOfWork
    {
        public Task<int> Save()
        {
            return Task.FromResult(1);
        }
    }
}
