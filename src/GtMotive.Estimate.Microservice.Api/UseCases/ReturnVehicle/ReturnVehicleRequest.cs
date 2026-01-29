using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    /// <summary>
    /// Request for returning a vehicle.
    /// </summary>
    /// <param name="VehicleId">Vehicle identifier.</param>
    public record ReturnVehicleRequest(
            [Required]
            [property: JsonRequired] Guid VehicleId)
        : IRequest<IWebApiPresenter>;
}
