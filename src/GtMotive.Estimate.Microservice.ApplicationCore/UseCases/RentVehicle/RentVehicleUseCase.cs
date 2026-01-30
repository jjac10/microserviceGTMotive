using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Use case for renting a vehicle.
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="rentalRepository">The rental repository.</param>
    /// <param name="unitOfWork">The unit of work for persisting changes.</param>
    /// <param name="outputPort">The output port.</param>
    /// <param name="logger">The app logger.</param>
    public class RentVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        IUnitOfWork unitOfWork,
        IRentVehicleOutputPort outputPort,
        IAppLogger<RentVehicleUseCase> logger)
        : IUseCase<RentVehicleInput>
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IRentalRepository _rentalRepository = rentalRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRentVehicleOutputPort _outputPort = outputPort;
        private readonly IAppLogger<RentVehicleUseCase> _logger = logger;

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="input">The input data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(RentVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            _logger.LogInformation($"Attempting to rent vehicle {input.VehicleId} to customer {input.CustomerId}");

            try
            {
                var vehicle = await _vehicleRepository.GetByIdAsync(input.VehicleId);
                if (vehicle == null)
                {
                    _logger.LogWarning($"Vehicle {input.VehicleId} not found.");
                    _outputPort.NotFoundHandle($"Vehicle {input.VehicleId} not found.");
                    return;
                }

                if (!vehicle.IsAvailable)
                {
                    _logger.LogWarning($"Vehicle {input.VehicleId} is not available for rent.");
                    _outputPort.VehicleNotAvailable($"Vehicle {input.VehicleId} is not available for rent.");
                    return;
                }

                var customerWithRental = await _rentalRepository.GetActiveByCustomerIdAsync(input.CustomerId);
                if (customerWithRental != null)
                {
                    _logger.LogWarning($"Customer {input.CustomerId} already has an active rental.");
                    _outputPort.CustomerAlreadyHasActiveRental($"Customer {input.CustomerId} already has an active rental.");
                    return;
                }

                var rental = new Rental(
                    input.VehicleId,
                    input.CustomerId,
                    DateTime.UtcNow);

                vehicle.MarkAsRented();

                await _rentalRepository.AddAsync(rental);
                await _vehicleRepository.UpdateAsync(vehicle);

                await _unitOfWork.Save();

                _logger.LogInformation($"Vehicle {input.VehicleId} rented to customer {input.CustomerId} successfully.");

                var output = new RentVehicleOutput(rental.Id, rental.VehicleId, rental.CustomerId, rental.StartDate);

                _outputPort.StandardHandle(output);
            }
            catch (DomainException ex)
            {
                _logger.LogError(ex, "Domain error occurred while renting vehicle.");
                _outputPort.InvalidRentalRequest(ex.Message);
            }
        }
    }
}
