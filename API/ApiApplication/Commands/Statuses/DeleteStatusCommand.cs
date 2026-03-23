using Domain.Roots.Statuses.Services;
using MediatR;

namespace ApiApplication.Commands.Statuses;

public class DeleteStatusCommand : IRequest
{
    public int Id { get; set; }

    public DeleteStatusCommand(int id) => Id = id;
}

public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand>
{
    private readonly IStatusService _statusService;

    public DeleteStatusCommandHandler(IStatusService statusService)
    {
        _statusService = statusService;
    }

    public async Task Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
    {
        await _statusService.DeleteStatusById(request.Id);
    }
}