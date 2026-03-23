using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Roots.Bookings;

namespace Domain.Roots.Statuses;

public class Status : BasicEntity
{
    public string Name { get; protected set; } = null!;
    public string Color { get; protected set; } = null!;
    public int Order { get; protected set; }
    public DateTime? ArchivedAt { get; protected set; }

    public virtual ICollection<Booking> Bookings { get; protected set; } = null!;

    private Status()
    {
    }

    public Status(string name, string color)
    {
        Name = name;
        Color = color;
    }

    public void SetOrder(int order)
    {
        Order = order;
    }

    public void Archive()
    {
        if (ArchivedAt != null)
        {
            throw new ValidationException("Status jest już zarichiwizowany");
        }

        ArchivedAt = DateTime.UtcNow;
    }

    public void Update(string name, string color)
    {
        Name = name;
        Color = color;
    }
}