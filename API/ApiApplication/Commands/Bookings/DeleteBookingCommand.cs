using Domain.Roots.Bookings.Services;
using MediatR;

namespace ApiApplication.Bookings.Commands;

public class DeleteBookingCommand : IRequest
{
    public int Id { get; set; }

    public DeleteBookingCommand(int id) => Id = id;
}

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
    private readonly IBookingService _bookingService;

    public DeleteBookingCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        await _bookingService.DeleteReservationById(request.Id);
    }
}