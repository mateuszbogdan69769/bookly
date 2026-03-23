using Domain.Aggregates.ReservationServices.Models;

namespace Domain.Aggregates.ReservationServices;

public interface IReservationService
{
    Task<int> CreateNewBooking(GuestConfig guestConfig, BookingConfig bookingConfig);
}
