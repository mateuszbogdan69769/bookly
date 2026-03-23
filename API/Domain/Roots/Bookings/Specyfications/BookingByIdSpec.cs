using Ardalis.Specification;

namespace Domain.Roots.Bookings.Specyfications;

internal class BookingByIdSpec : Specification<Booking>
{
    public BookingByIdSpec(int id)
    {
        Query.Where(x => x.Id == id);

        Query.Include(x => x.Guest);
    }
}
