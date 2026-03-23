using Ardalis.Specification;

namespace Domain.Roots.Bookings.Specyfications;

internal class BookingsFilteredSpec : Specification<Booking>
{
    public BookingsFilteredSpec(string? searchQuery, DateTime dateFrom, DateTime dateTo)
    {
        Query.Include(x => x.Guest);

        Query.Where(x => x.StartDate >= dateFrom && x.EndDate <= dateTo);

        if (!String.IsNullOrEmpty(searchQuery))
        {
            var normalizedSearch = searchQuery.ToLower().Trim();
            Query.Where(x => x.Note.ToLower().Trim().Contains(normalizedSearch) || (x.Guest.Name + " " + x.Guest.Surname).ToLower().Trim().Contains(normalizedSearch));
        }
    }
}
