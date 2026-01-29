using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    /// <summary>
    /// Request for creating a new vehicle.
    /// </summary>
    /// <param name="Brand">Vehicle brand.</param>
    /// <param name="Model">Vehicle model.</param>
    /// <param name="LicensePlate">Vehicle license plate.</param>
    /// <param name="ManufacturingDate">Vehicle manufacturing date.</param>
    public record CreateVehicleRequest(
            [Required] string Brand,
            [Required] string Model,
            [Required] string LicensePlate,
            [Required]
            [property: JsonRequired]
            DateTime ManufacturingDate)
        : IRequest<IWebApiPresenter>;
}
