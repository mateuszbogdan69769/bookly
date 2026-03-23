namespace Domain.Aggregates.StatisticsServices.Models;

public class DashboardStats
{
    public int TotalBookings { get; set; }
    public int BookingsToday { get; set; }
    public int TotalGuests { get; set; }
    public int PeoplesToday { get; set; }

    public DashboardStats(int totalBookings, int bookingsToday, int totalGuests, int peoplesToday)
    {
        TotalBookings = totalBookings;
        BookingsToday = bookingsToday;
        TotalGuests = totalGuests;
        PeoplesToday = peoplesToday;
    }
}
