using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    public record GetAvailableVehiclesRequest : IRequest<IWebApiPresenter>;
}
