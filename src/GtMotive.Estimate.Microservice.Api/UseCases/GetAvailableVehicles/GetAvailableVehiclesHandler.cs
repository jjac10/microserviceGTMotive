using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Handler for get available vehicles request.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="GetAvailableVehiclesHandler"/> class.
    /// </remarks>
    /// <param name="useCase">The use case.</param>
    /// <param name="presenter">The presenter.</param>
    public class GetAvailableVehiclesHandler(IUseCase<GetAvailableVehiclesInput> useCase, GetAvailableVehiclesPresenter presenter)
        : IRequestHandler<GetAvailableVehiclesRequest, IWebApiPresenter>
    {
        private readonly IUseCase<GetAvailableVehiclesInput> _useCase = useCase;
        private readonly GetAvailableVehiclesPresenter _presenter = presenter;

        public async Task<IWebApiPresenter> Handle(GetAvailableVehiclesRequest request, CancellationToken cancellationToken)
        {
            var input = new GetAvailableVehiclesInput();
            await _useCase.Execute(input);
            return _presenter;
        }
    }
}
