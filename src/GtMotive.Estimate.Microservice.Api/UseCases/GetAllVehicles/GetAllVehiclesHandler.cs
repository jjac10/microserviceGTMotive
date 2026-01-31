using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAllVehicles;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAllVehicles
{
    /// <summary>
    /// Handler for get all vehicles request.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="GetAllVehiclesHandler"/> class.
    /// </remarks>
    /// <param name="useCase">The use case.</param>
    /// <param name="presenter">The presenter.</param>
    public class GetAllVehiclesHandler(IUseCase<GetAllVehiclesInput> useCase, GetAllVehiclesPresenter presenter)
        : IRequestHandler<GetAllVehiclesRequest, IWebApiPresenter>
    {
        private readonly IUseCase<GetAllVehiclesInput> _useCase = useCase;
        private readonly GetAllVehiclesPresenter _presenter = presenter;

        public async Task<IWebApiPresenter> Handle(GetAllVehiclesRequest request, CancellationToken cancellationToken)
        {
            var input = new GetAllVehiclesInput();
            await _useCase.Execute(input);
            return _presenter;
        }
    }
}
