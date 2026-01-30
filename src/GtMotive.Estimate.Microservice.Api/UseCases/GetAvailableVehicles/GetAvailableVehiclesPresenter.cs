using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    public class GetAvailableVehiclesPresenter : IGetAvailableVehiclesOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        public void StandardHandle(GetAvailableVehiclesOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            ActionResult = new OkObjectResult(response.Vehicles);
        }
    }
}
