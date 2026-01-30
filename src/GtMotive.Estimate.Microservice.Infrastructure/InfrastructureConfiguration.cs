using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories.InMemory;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.Telemetry;
using Microsoft.Extensions.DependencyInjection;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        [ExcludeFromCodeCoverage]
        public static IInfrastructureBuilder AddBaseInfrastructure(
            this IServiceCollection services,
            bool isDevelopment)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            if (!isDevelopment)
            {
                services.AddScoped<ITelemetry, AppTelemetry>();
            }
            else
            {
                services.AddScoped<ITelemetry, NoOpTelemetry>();
            }

            return new InfrastructureBuilder(services);
        }

        public static IServiceCollection AddMongoDbInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<MongoService>();
            services.AddScoped<IVehicleRepository, MongoVehicleRepository>();
            services.AddScoped<IRentalRepository, MongoRentalRepository>();
            services.AddScoped<IUnitOfWork, MongoUnitOfWork>();

            return services;
        }

        public static IServiceCollection AddInMemoryInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IVehicleRepository, InMemoryVehicleRepository>();
            services.AddSingleton<IRentalRepository, InMemoryRentalRepository>();
            services.AddScoped<IUnitOfWork, InMemoryUnitOfWork>();

            return services;
        }

        private sealed class InfrastructureBuilder(IServiceCollection services) : IInfrastructureBuilder
        {
            public IServiceCollection Services { get; } = services;
        }
    }
}
