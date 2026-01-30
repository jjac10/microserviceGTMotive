using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAllVehicles
{
    public record GetAllVehiclesRequest : IRequest<IWebApiPresenter>;
}
