using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Use case for getting available vehicles.
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="outputPort">The output port.</param>
    /// <param name="logger">The app logger.</param>
    public class GetAvailableVehiclesUseCase(
        IVehicleRepository vehicleRepository,
        IGetAvailableVehiclesOutputPort outputPort,
        IAppLogger<GetAvailableVehiclesUseCase> logger)
         : IUseCase<GetAvailableVehiclesInput>
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IGetAvailableVehiclesOutputPort _outputPort = outputPort;
        private readonly IAppLogger<GetAvailableVehiclesUseCase> _logger = logger;

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="input">The input data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(GetAvailableVehiclesInput input)
        {
            _logger.LogInformation("Retrieving available vehicles.");

            var availableVehicles = await _vehicleRepository.GetAvailablesAsync();

            var vehicleOutputs = availableVehicles.Select(vehicle => new VehicleOutput(
                    vehicle.Id,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.LicensePlate,
                    vehicle.ManufacturingDate))
                .ToList();

            _logger.LogInformation($"Available vehicles found: {vehicleOutputs.Count}.");

            var output = new GetAvailableVehiclesOutput(vehicleOutputs);
            _outputPort.StandardHandle(output);
        }
    }
}
