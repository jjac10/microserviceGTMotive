namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output port for the ReturnVehicle use case.
    /// </summary>
    public interface IReturnVehicleOutputPort : IOutputPortStandard<ReturnVehicleOutput>, IOutputPortNotFound
    {
        /// <summary>
        /// Handles the case when the return request is invalid.
        /// </summary>
        /// <param name="message">The error message.</param>
        void InvalidReturnRequest(string message);
    }
}
