using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Use case for creating a new vehicle.
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="unitOfWork">The unit of work for persisting changes.</param>
    /// <param name="outputPort">The output port.</param>
    /// <param name="logger">The app logger.</param>
    public class CreateVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IUnitOfWork unitOfWork,
        ICreateVehicleOutputPort outputPort,
        IAppLogger<CreateVehicleUseCase> logger)
        : IUseCase<CreateVehicleInput>
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICreateVehicleOutputPort _outputPort = outputPort;
        private readonly IAppLogger<CreateVehicleUseCase> _logger = logger;

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="input">The input data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(CreateVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            _logger.LogInformation($"Starting vehicle creation process for license plate: {input.LicensePlate}");

            try
            {
                var exists = await _vehicleRepository.ExistsByLicensePlateAsync(input.LicensePlate);
                if (exists)
                {
                    _logger.LogWarning($"Vehicle with License plate {input.LicensePlate} already exists.");
                    _outputPort.LicensePlateAlreadyExists($"A vehicle with license plate {input.LicensePlate} already exists.");
                    return;
                }

                var vehicle = new Vehicle(
                    input.Brand,
                    input.Model,
                    input.LicensePlate,
                    input.ManufacturingDate);

                await _vehicleRepository.AddAsync(vehicle);

                await _unitOfWork.Save();

                _logger.LogInformation($"Vehicle with License plate {input.LicensePlate} created successfully with ID: {vehicle.Id}");

                var output = new CreateVehicleOutput(vehicle.Id, vehicle.Brand, vehicle.Model, vehicle.LicensePlate, vehicle.ManufacturingDate);

                _outputPort.StandardHandle(output);
            }
            catch (VehicleTooOldException ex)
            {
                _logger.LogWarning($"Failed to create vehicle: {ex.Message}");
                _outputPort.VehicleTooOld(ex.Message);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning($"Failed to create vehicle (Domain validation): {ex.Message}");
                _outputPort.DomainError(ex.Message);
            }
        }
    }
}
