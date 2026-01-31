using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAllVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAllVehicles
{
    public class GetAllVehiclesPresenter : IGetAllVehiclesOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        public void StandardHandle(GetAllVehiclesOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            ActionResult = new OkObjectResult(response.Vehicles);
        }
    }
}
