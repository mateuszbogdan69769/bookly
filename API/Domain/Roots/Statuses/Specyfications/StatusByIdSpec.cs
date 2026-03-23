using Ardalis.Specification;

namespace Domain.Roots.Statuses.Specyfications;

public class StatusByIdSpec : Specification<Status>
{
    public StatusByIdSpec(int id, bool includeBookings = false)
    {
        if (includeBookings)
        {
            Query.Include(x => x.Bookings);
        }

        Query.Where(x => x.Id == id);
    }
}