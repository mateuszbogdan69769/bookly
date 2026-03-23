using Domain.Aggregates.ReservationServices.Models;
using Domain.Roots.Bookings;
using Domain.Roots.Bookings.Services;
using Domain.Roots.Guests;
using Domain.Roots.Guests.Services;

namespace Domain.Aggregates.ReservationServices;

public class ReservationService : IReservationService
{
    private readonly IBookingService _bookingService;
    private readonly IGuestService _guestService;

    public ReservationService(IBookingService bookingService, IGuestService guestService)
    {
        _bookingService = bookingService;
        _guestService = guestService;
    }

    public async Task<int> CreateNewBooking(GuestConfig guestConfig, BookingConfig bookingConfig)
    {
        var guest = await GetExistingGuestOrCreateNew(guestConfig.Name, guestConfig.Surname);
        var booking = new Booking(bookingConfig.StartDate, bookingConfig.EndDate, guest, bookingConfig.PartySize, bookingConfig.Note, bookingConfig.StatusId);
        var bookingId = await _bookingService.AddBooking(booking);
        return bookingId;
    }

    private async Task<Guest> GetExistingGuestOrCreateNew(string name, string surname)
    {
        var existingGuest = await _guestService.GetGuestByNameAndSurname(name, surname);
        if (existingGuest is not null) return existingGuest;

        var newGuest = new Guest(name, surname);
        await _guestService.AddGuest(newGuest);
        return newGuest;
    }
}
