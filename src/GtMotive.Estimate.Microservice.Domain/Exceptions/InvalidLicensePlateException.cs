using System;

namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a license plate has diferent format from spanish format.
    /// </summary>
    public class InvalidLicensePlateException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLicensePlateException"/> class.
        /// </summary>
        /// <param name="licensePlate">The invalid license plate.</param>
        public InvalidLicensePlateException(string licensePlate)
            : base($"The license plate '{licensePlate}' is invalid. Expected format: 1234ABC")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLicensePlateException"/> class.
        /// </summary>
        public InvalidLicensePlateException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLicensePlateException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public InvalidLicensePlateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
