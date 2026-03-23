using Domain.Aggregates.ReservationServices;
using Domain.Aggregates.ReservationServices.Models;
using FluentValidation;
using MediatR;

namespace ApiApplication.Bookings.Commands;

public class CreateBookingCommand : IRequest
{
    // Guest
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;

    // Booking
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PartySize { get; set; }
    public string Note { get; set; } = string.Empty;
    public int StatusId { get; set; }
}

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Imię jest wymagane");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Nazwisko jest wymagane");
    }
}

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand>
{
    private readonly IReservationService _reservationService;

    public CreateBookingCommandHandler(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public async Task Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var guestConfig = new GuestConfig()
        {
            Name = request.Name,
            Surname = request.Surname
        };

        var bookingConfig = new BookingConfig()
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PartySize = request.PartySize,
            Note = request.Note,
            StatusId = request.StatusId
        };

        await _reservationService.CreateNewBooking(guestConfig, bookingConfig);
    }
}