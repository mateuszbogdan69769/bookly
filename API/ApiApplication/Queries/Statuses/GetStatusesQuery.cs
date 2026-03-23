using ApiApplication.DTOs;
using Domain.Roots.Statuses.Services;
using MediatR;

namespace ApiApplication.Queries.Statuses;

public class GetStatusesQuery : IRequest<List<StatusDto>>
{
}

public class GetStatusesQueryHandler : IRequestHandler<GetStatusesQuery, List<StatusDto>>
{
    private readonly IStatusService _statusService;

    public GetStatusesQueryHandler(IStatusService statusService)
    {
        _statusService = statusService;
    }

    public async Task<List<StatusDto>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
    {
        var result = await _statusService.GetAllStatuses();

        return result.Select(StatusDto.Map).ToList();
    }
}