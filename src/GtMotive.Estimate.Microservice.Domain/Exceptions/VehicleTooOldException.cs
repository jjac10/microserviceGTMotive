using System;

namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a vehicle exceeds the maximum age allowed.
    /// </summary>
    public class VehicleTooOldException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldException"/> class.
        /// </summary>
        public VehicleTooOldException()
            : base("The vehicle is over 5 years old and cannot be registered.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public VehicleTooOldException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public VehicleTooOldException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
