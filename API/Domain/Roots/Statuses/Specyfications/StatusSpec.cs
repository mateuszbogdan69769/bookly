using Ardalis.Specification;

namespace Domain.Roots.Statuses.Specyfications;

public class StatusSpec : Specification<Status>
{
    public StatusSpec(bool includeBookings = false)
    {
        if (includeBookings)
        {
            Query.Include(x => x.Bookings);
        }

        Query.Where(x => x.ArchivedAt == null);
    }
}