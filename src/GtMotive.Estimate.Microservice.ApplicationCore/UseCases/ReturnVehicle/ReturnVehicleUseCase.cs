using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Use case for returning a vehicle.
    /// </summary>
    /// <param name="vehicleRepository">Vehicle repository.</param>
    /// <param name="rentalRepository">Rental repository.</param>
    /// <param name="unitOfWork">The unit of work for persisting changes.</param>
    /// <param name="outputPort">The output port.</param>
    /// <param name="logger">The app logger.</param>
    public class ReturnVehicleUseCase(IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        IUnitOfWork unitOfWork,
        IReturnVehicleOutputPort outputPort,
        IAppLogger<ReturnVehicleUseCase> logger)
        : IUseCase<ReturnVehicleInput>
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IRentalRepository _rentalRepository = rentalRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IReturnVehicleOutputPort _outputPort = outputPort;
        private readonly IAppLogger<ReturnVehicleUseCase> _logger = logger;

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="input">The input data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(ReturnVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            _logger.LogInformation($"Starting return vehicle procces for VehicleId: {input.VehicleId}");

            try
            {
                var rental = await _rentalRepository.GetActiveByVehicleIdAsync(input.VehicleId);
                if (rental == null)
                {
                    _logger.LogWarning($"No active rental found for VehicleId: {input.VehicleId}");
                    _outputPort.NotFoundHandle($"No active rental found for VehicleId: {input.VehicleId}");
                    return;
                }

                var vehicle = await _vehicleRepository.GetByIdAsync(input.VehicleId);
                if (vehicle == null)
                {
                    _logger.LogWarning("Vehicle not found for VehicleId: {VehicleId}", input.VehicleId);
                    _outputPort.NotFoundHandle($"Vehicle not found for VehicleId: {input.VehicleId}");
                    return;
                }

                var endDate = DateTime.UtcNow;

                rental.Finish(endDate);
                vehicle.MarkAsAvailable();

                await _rentalRepository.UpdateAsync(rental);
                await _vehicleRepository.UpdateAsync(vehicle);

                await _unitOfWork.Save();

                _logger.LogInformation($"VehicleId: {input.VehicleId} returned successfully at {endDate}.");

                var output = new ReturnVehicleOutput(
                    rental.Id,
                    vehicle.Id,
                    rental.CustomerId,
                    rental.StartDate,
                    endDate);

                _outputPort.StandardHandle(output);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning($"Domain exception occurred while returning VehicleId: {input.VehicleId}. Exception: {ex.Message}");
                _outputPort.InvalidReturnRequest($"Domain exception: {ex.Message}");
            }
        }
    }
}
