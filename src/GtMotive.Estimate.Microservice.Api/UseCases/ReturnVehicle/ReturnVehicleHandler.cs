using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    /// <summary>
    /// Handler for return vehicle request.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ReturnVehicleHandler"/> class.
    /// </remarks>
    /// <param name="useCase">The use case.</param>
    /// <param name="presenter">The presenter.</param>
    public class ReturnVehicleHandler(IUseCase<ReturnVehicleInput> useCase, ReturnVehiclePresenter presenter)
        : IRequestHandler<ReturnVehicleRequest, IWebApiPresenter>
    {
        private readonly IUseCase<ReturnVehicleInput> _useCase = useCase;
        private readonly ReturnVehiclePresenter _presenter = presenter;

        public async Task<IWebApiPresenter> Handle(ReturnVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new ReturnVehicleInput(request.VehicleId);
            await _useCase.Execute(input);
            return _presenter;
        }
    }
}
