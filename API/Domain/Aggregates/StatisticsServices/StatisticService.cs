using Domain.Aggregates.StatisticsServices.Models;
using Domain.Roots.Bookings.Services;
using Domain.Roots.Guests.Services;

namespace Domain.Aggregates.StatisticsServices;

public class StatisticService : IStatisticService
{
    private readonly IBookingService _bookingService;
    private readonly IGuestService _guestService;

    public StatisticService(IBookingService bookingService, IGuestService guestService)
    {
        _bookingService = bookingService;
        _guestService = guestService;
    }

    public async Task<DashboardStats> GetStats()
    {
        var allBookings = await _bookingService.GetAllBookings();
        var todayDateOnly = DateOnly.FromDateTime(DateTime.Now);

        var bookingsToday = allBookings.Where(x => DateOnly.FromDateTime(x.StartDate) == todayDateOnly).ToList();
        var peoplesToday = bookingsToday.Select(x => x.PartySize).Sum();

        var allGuestsCount = allBookings.Select(x => x.PartySize).Sum();

        var result = new DashboardStats(allBookings.Count, bookingsToday.Count, allGuestsCount, peoplesToday);

        return result;
    }
}
