using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.GetAllVehicles;
using GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles;
using GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Controller for vehicle operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="VehiclesController"/> class.
    /// </remarks>
    /// <param name="mediator">The mediator.</param>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class VehiclesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var presenter = await _mediator.Send(new GetAllVehiclesRequest());
            return presenter.ActionResult;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request)
        {
            var presenter = await _mediator.Send(request);
            return presenter.ActionResult;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailablesVehicles()
        {
            var presenter = await _mediator.Send(new GetAvailableVehiclesRequest());
            return presenter.ActionResult;
        }

        [HttpPost("rent")]
        public async Task<IActionResult> RentVehicle([FromBody] RentVehicleRequest request)
        {
            var presenter = await _mediator.Send(request);
            return presenter.ActionResult;
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnVehicle([FromBody] ReturnVehicleRequest request)
        {
            var presenter = await _mediator.Send(request);
            return presenter.ActionResult;
        }
    }
}
