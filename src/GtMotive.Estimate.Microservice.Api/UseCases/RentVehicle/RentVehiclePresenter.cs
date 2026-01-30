using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    public class RentVehiclePresenter : IRentVehicleOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        public void StandardHandle(RentVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            ActionResult = new OkObjectResult(response);
        }

        public void CustomerAlreadyHasActiveRental(string message)
        {
            ActionResult = new ConflictObjectResult(new
            {
                Error = "Customer Limit Reached",
                Detail = message
            });
        }

        public void InvalidRentalRequest(string message)
        {
            ActionResult = new BadRequestObjectResult(new
            {
                Error = "Business Validation Failed",
                Detail = message
            });
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new
            {
                Error = "Vehicle Not Found",
                Detail = message
            });
        }

        public void VehicleNotAvailable(string message)
        {
            ActionResult = new ConflictObjectResult(new
            {
                Error = "Vehicle Not Available",
                Detail = message
            });
        }
    }
}
