using Domain.Roots.Bookings.Services;
using MediatR;

namespace ApiApplication.Bookings.Commands;

public class UpdateBookingStatusCommand : IRequest
{
    public int Id { get; set; }
    public int StatusId { get; set; }
}

public class UpdateBookingStatusCommandHandler : IRequestHandler<UpdateBookingStatusCommand>
{
    private readonly IBookingService _bookingService;

    public UpdateBookingStatusCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task Handle(UpdateBookingStatusCommand request, CancellationToken cancellationToken)
    {
        await _bookingService.UpdateBookingStatus(request.Id, request.StatusId);
    }
}