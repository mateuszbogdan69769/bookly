using Domain.Roots.Guests;

namespace ApiApplication.DTOs;

public class GuestDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public List<BookingDto>? Bookings { get; set; }

    public static GuestDto Map(Guest guest, bool mapBookings = false)
    {
        return new GuestDto()
        {
            Id = guest.Id,
            Name = guest.Name,
            Surname = guest.Surname,
            CreatedAt = guest.CreatedAt,
            Bookings = mapBookings && guest.Bookings != null ? guest.Bookings.Select(BookingDto.Map).ToList() : null
        };
    }
}