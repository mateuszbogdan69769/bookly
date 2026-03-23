using Domain.Roots.Bookings;

namespace ApiApplication.DTOs;

public class BookingDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PartySize { get; set; }
    public string Note { get; set; } = string.Empty;
    public GuestDto Guest { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int? StatusId { get; set; }

    public static BookingDto Map(Booking booking)
    {
        return new BookingDto()
        {
            Id = booking.Id,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            PartySize = booking.PartySize,
            Note = booking.Note,
            Guest = GuestDto.Map(booking.Guest),
            CreatedAt = booking.CreatedAt,
            StatusId = booking.StatusId
        };
    }
}