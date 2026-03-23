using Domain.Roots.Guests.Services;
using MediatR;

namespace ApiApplication.Bookings.Guests;

public class DeleteGuestCommand : IRequest
{
    public int Id { get; set; }
    public DeleteGuestCommand(int id) => Id = id;
}

public class DeleteGuestCommandHandler : IRequestHandler<DeleteGuestCommand>
{
    private readonly IGuestService _guestService;

    public DeleteGuestCommandHandler(IGuestService guestService)
    {
        _guestService = guestService;
    }

    public async Task Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
    {
        await _guestService.DeleteGuestById(request.Id);
    }
}