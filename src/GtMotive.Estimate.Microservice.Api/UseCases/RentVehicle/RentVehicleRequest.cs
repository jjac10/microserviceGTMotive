using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// Request for renting a vehicle.
    /// </summary>
    /// <param name="VehicleId">Vehicle identifier.</param>
    /// <param name="CustomerId">Customer identifier.</param>
    public record RentVehicleRequest(
            [Required]
            [property: JsonRequired] Guid VehicleId,
            [Required]
            [property: JsonRequired] Guid CustomerId)
        : IRequest<IWebApiPresenter>;
}
