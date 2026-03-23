using Domain.Interfaces;
using Infrastructure.Common;
using Domain.Roots.Guests;
using Domain.Roots.Guests.Repos;

namespace Infrastructure.Persistence.Repos;

public class GuestRepository : BookingRepositoryBasic<Guest>, IGuestRepository
{
    public GuestRepository(IBookingDbContext bookingDbContext) : base(bookingDbContext)
    {
    }
}

