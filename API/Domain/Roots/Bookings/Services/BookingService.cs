using Domain.Roots.Bookings.Repos;
using Domain.Roots.Bookings.Specyfications;
using System.ComponentModel.DataAnnotations;

namespace Domain.Roots.Bookings.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public Task<List<Booking>> GetAllBookings()
    {
        return _bookingRepository.ListAsync();
    }

    public async Task<List<Booking>> GetFilteredBookings(string? searchQuery, DateTime dateFrom, DateTime dateTo)
    {
        var spec = new BookingsFilteredSpec(searchQuery, dateFrom, dateTo);
        return await _bookingRepository.ListAsync(spec);
    }

    public async Task<int> AddBooking(Booking booking)
    {
        await _bookingRepository.AddAsync(booking);
        return booking.Id;
    }

    public async Task<Booking?> GetBookingById(int id)
    {
        var spec = new BookingByIdSpec(id);
        return await _bookingRepository.FirstOrDefaultAsync(spec);
    }

    public async Task UpdateBooking(int id, DateTime startDate, DateTime endDate, int partySize, string note, string name, string surname, int statusId)
    {
        var booking = await GetBookingById(id);
        if (booking is null)
        {
            throw new ValidationException("Rezerwacja nie znaleziona");
        }
        if (booking.Guest is null)
        {
            throw new ValidationException("Rezerwacja nie ma gościa");
        }
        booking.Update(startDate, endDate, partySize, note, statusId);
        booking.Guest.Update(name, surname);
        await _bookingRepository.UpdateAsync(booking);
    }

    public async Task UpdateBookingStatus(int id, int statusId)
    {
        var booking = await GetBookingById(id);
        if (booking is null)
        {
            throw new ValidationException("Rezerwacja nie znaleziona");
        }
        booking.UpdateStatus(statusId);
        await _bookingRepository.UpdateAsync(booking);
    }

    public async Task DeleteReservationById(int id)
    {
        var booking = await GetBookingById(id);
        if (booking is null)
        {
            throw new ValidationException("Rezerwacja nie znaleziona");
        }
        await _bookingRepository.DeleteAsync(booking);
    }
}
