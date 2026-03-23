using Domain.Common;
using Domain.Roots.Guests;
using Domain.Roots.Statuses;

namespace Domain.Roots.Bookings;

public class Booking : BasicEntity
{
    public DateTime StartDate { get; protected set; }
    public DateTime EndDate { get; protected set; }
    public int GuestId { get; protected set; }
    public int PartySize { get; protected set; }
    public string Note { get; protected set; } = string.Empty;
    public DateTime CreatedAt { get; protected set; }
    public int? StatusId { get; protected set; }

    public virtual Guest Guest { get; protected set; } = null!;
    public virtual Status Status { get; protected set; }

    private Booking() { }

    public Booking(DateTime startDate, DateTime endDate, Guest guest, int partySize, string note, int statusId)
    {
        StartDate = startDate;
        EndDate = endDate;
        GuestId = guest.Id;
        Guest = guest;
        PartySize = partySize;
        Note = note;
        CreatedAt = DateTime.UtcNow;
        StatusId = statusId;
    }

    public void Update(DateTime startDate, DateTime endDate, int partySize, string note, int statusId)
    {
        StartDate = startDate;
        EndDate = endDate;
        PartySize = partySize;
        Note = note;
        StatusId = statusId;
    }

    public void UpdateStatus(int statusId)
    {
        StatusId = statusId;
    }
}

