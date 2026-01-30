using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAllVehicles
{
    /// <summary>
    /// Use case for getting all vehicles.
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="outputPort">The output port.</param>
    /// <param name="logger">The app logger.</param>
    public class GetAllVehiclesUseCase(
        IVehicleRepository vehicleRepository,
        IGetAllVehiclesOutputPort outputPort,
        IAppLogger<GetAllVehiclesUseCase> logger)
         : IUseCase<GetAllVehiclesInput>
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IGetAllVehiclesOutputPort _outputPort = outputPort;
        private readonly IAppLogger<GetAllVehiclesUseCase> _logger = logger;

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="input">The input data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(GetAllVehiclesInput input)
        {
            _logger.LogInformation("Retrieving all vehicles.");

            var vehicles = await _vehicleRepository.GetAllAsync();

            var vehicleOutputs = vehicles.Select(vehicle => new VehicleOutput(
                    vehicle.Id,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.LicensePlate,
                    vehicle.ManufacturingDate,
                    vehicle.IsAvailable))
                .ToList();

            _logger.LogInformation($"Vehicles found: {vehicleOutputs.Count}.");

            var output = new GetAllVehiclesOutput(vehicleOutputs);
            _outputPort.StandardHandle(output);
        }
    }
}
