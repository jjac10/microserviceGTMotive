namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output port for rent vehicle use case.
    /// </summary>
    public interface IRentVehicleOutputPort : IOutputPortStandard<RentVehicleOutput>, IOutputPortNotFound
    {
        /// <summary>
        /// Handles the case when the vehicle is not available for rent.
        /// </summary>
        /// <param name="message">The error message.</param>
        void VehicleNotAvailable(string message);

        /// <summary>
        /// Handles the case when the customer already has an active rental.
        /// </summary>
        /// <param name="message">The error message.</param>
        void CustomerAlreadyHasActiveRental(string message);

        /// <summary>
        /// Handles the case when the rental request is invalid.
        /// </summary>
        /// <param name="message">The error message.</param>
        void InvalidRentalRequest(string message);
    }
}
