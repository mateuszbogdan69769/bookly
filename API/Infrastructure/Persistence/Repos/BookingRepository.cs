using Domain.Interfaces;
using Infrastructure.Common;
using Domain.Roots.Bookings.Repos;
using Domain.Roots.Bookings;

namespace Infrastructure.Persistence.Repos;

public class BookingRepository : BookingRepositoryBasic<Booking>, IBookingRepository
{
    public BookingRepository(IBookingDbContext bookingDbContext) : base(bookingDbContext)
    {
    }
}

