using Ardalis.Specification;

namespace Domain.Roots.Guests.Specyfications;

public class GuestsFilteredSpec : Specification<Guest>
{
    public GuestsFilteredSpec(string? searchQuery, bool includeBookings = false)
    {
        if (!String.IsNullOrEmpty(searchQuery))
        {
            Query.Where(x => (x.Name + " " + x.Surname).ToLower().Trim().Contains(searchQuery.ToLower().Trim()));
        }

        if (includeBookings)
        {
            Query.Include(x => x.Bookings);
        }
    }
}
