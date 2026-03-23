using Domain.Common;
using Domain.Roots.Bookings;

namespace Domain.Roots.Guests;

public class Guest : BasicEntity
{
    public string Name { get; protected set; } = null!;
    public string Surname { get; protected set; } = null!;
    public DateTime CreatedAt { get; protected set; }

    public virtual ICollection<Booking> Bookings { get; protected set; } = null!;

    private Guest() { }

    public Guest(string name, string surname)
    {
        Name = name;
        Surname = surname;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
}

