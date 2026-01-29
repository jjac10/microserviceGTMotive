using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// Handler for rent vehicle request.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RentVehicleHandler"/> class.
    /// </remarks>
    /// <param name="useCase">The use case.</param>
    /// <param name="presenter">The presenter.</param>
    public class RentVehicleHandler(IUseCase<RentVehicleInput> useCase, RentVehiclePresenter presenter)
        : IRequestHandler<RentVehicleRequest, IWebApiPresenter>
    {
        private readonly IUseCase<RentVehicleInput> _useCase = useCase;
        private readonly RentVehiclePresenter _presenter = presenter;

        public async Task<IWebApiPresenter> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new RentVehicleInput(request.VehicleId, request.CustomerId);
            await _useCase.Execute(input);
            return _presenter;
        }
    }
}
