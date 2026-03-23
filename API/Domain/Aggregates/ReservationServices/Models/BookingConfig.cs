namespace Domain.Aggregates.ReservationServices.Models;

public class BookingConfig
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PartySize { get; set; }
    public string Note { get; set; } = string.Empty;
    public int StatusId { get; set; }
}
