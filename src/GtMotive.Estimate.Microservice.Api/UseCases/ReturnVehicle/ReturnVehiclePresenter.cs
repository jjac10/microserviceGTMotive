using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    public class ReturnVehiclePresenter : IReturnVehicleOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        public void StandardHandle(ReturnVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            ActionResult = new OkObjectResult(response);
        }

        public void InvalidReturnRequest(string message)
        {
            ActionResult = new BadRequestObjectResult(new
            {
                Error = "Invalid Return Request",
                Detail = message
            });
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new
            {
                Error = "Rental not found",
                Detail = message
            });
        }
    }
}
