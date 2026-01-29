using System;
using GtMotive.Estimate.Microservice.Domain.Exceptions;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Rental entity representing a vehicle rental.
    /// </summary>
    public class Rental
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="startDate">Rental start date.</param>
        public Rental(Guid vehicleId, Guid customerId, DateTime startDate)
        {
            Id = Guid.NewGuid();
            VehicleId = vehicleId;
            CustomerId = customerId;
            StartDate = startDate;
            IsActive = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        public Rental()
        {
        }

        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the unique identifier for the vehicle.
        /// </summary>
        public Guid VehicleId { get; private set; }

        /// <summary>
        /// Gets the unique identifier for the customer.
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// Gets the start date for the associated event or period.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets or sets the end date for the associated period or event.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the object is currently active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Finish the rental.
        /// </summary>
        /// <param name="endDate">The end date of the rental.</param>
        public void Finish(DateTime endDate)
        {
            if (!IsActive)
            {
                throw new DomainException("The rental is already finished.");
            }

            if (endDate < StartDate)
            {
                throw new DomainException("The end date cannot be earlier than the start date.");
            }

            EndDate = endDate;
            IsActive = false;
        }
    }
}
