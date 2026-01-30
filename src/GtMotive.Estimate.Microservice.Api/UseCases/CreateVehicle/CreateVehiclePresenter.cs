using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    public class CreateVehiclePresenter : ICreateVehicleOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        public void StandardHandle(CreateVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            ActionResult = new OkObjectResult(response);
        }

        public void DomainError(string message)
        {
            ActionResult = new BadRequestObjectResult(new
            {
                Error = "Domain Validation Failed",
                Detail = message
            });
        }

        public void LicensePlateAlreadyExists(string message)
        {
            ActionResult = new ConflictObjectResult(new
            {
                Error = "Conflict",
                Detail = message
            });
        }

        public void VehicleTooOld(string message)
        {
            ActionResult = new ConflictObjectResult(new
            {
                Error = "Vehicle too old",
                Detail = message
            });
        }
    }
}
