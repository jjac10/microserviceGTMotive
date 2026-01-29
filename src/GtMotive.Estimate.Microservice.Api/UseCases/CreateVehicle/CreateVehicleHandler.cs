using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    /// <summary>
    /// Handler for create vehicle request.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateVehicleHandler"/> class.
    /// </remarks>
    /// <param name="useCase">The use case.</param>
    /// <param name="presenter">The presenter.</param>
    public class CreateVehicleHandler(IUseCase<CreateVehicleInput> useCase, CreateVehiclePresenter presenter)
        : IRequestHandler<CreateVehicleRequest, IWebApiPresenter>
    {
        private readonly IUseCase<CreateVehicleInput> _useCase = useCase;
        private readonly CreateVehiclePresenter _presenter = presenter;

        public async Task<IWebApiPresenter> Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new CreateVehicleInput(
                request.Brand,
                request.Model,
                request.LicensePlate,
                request.ManufacturingDate);

            await _useCase.Execute(input);

            return _presenter;
        }
    }
}
