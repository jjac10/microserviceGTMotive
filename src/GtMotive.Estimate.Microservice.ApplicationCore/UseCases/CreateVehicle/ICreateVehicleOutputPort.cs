namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Output port for create vehicle use case.
    /// </summary>
    public interface ICreateVehicleOutputPort : IOutputPortStandard<CreateVehicleOutput>
    {
        /// <summary>
        /// Handles the case when is a domain exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        void DomainError(string message);

        /// <summary>
        /// Handles the case when the vehicle is too old.
        /// </summary>
        /// <param name="message">The error message.</param>
        void VehicleTooOld(string message);

        /// <summary>
        /// Handles the case when a license plate already exists.
        /// </summary>
        /// <param name="message">The error message.</param>
        void LicensePlateAlreadyExists(string message);
    }
}
