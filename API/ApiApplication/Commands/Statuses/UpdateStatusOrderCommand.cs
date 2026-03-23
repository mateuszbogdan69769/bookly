using Domain.Roots.Statuses.Services;
using MediatR;

namespace ApiApplication.Commands.Statuses;

public class UpdateStatusOrderCommand : IRequest
{
    public List<StatusWithOrderSingleValue> StatusesIdsWithOrders { get; set; } = new List<StatusWithOrderSingleValue>();
}

public class StatusWithOrderSingleValue
{
    public int Id { get; set; }
    public int Order { get; set; }
}

public class UpdateStatusOrderCommandHandler : IRequestHandler<UpdateStatusOrderCommand>
{
    private readonly IStatusService _statusService;

    public UpdateStatusOrderCommandHandler(IStatusService statusService)
    {
        _statusService = statusService;
    }

    public async Task Handle(UpdateStatusOrderCommand request, CancellationToken cancellationToken)
    {
        var values = request.StatusesIdsWithOrders.Select(x => (x.Id, x.Order)).ToList();

        await _statusService.UpdateStatusesOrder(values);
    }
}