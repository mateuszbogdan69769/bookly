using Domain.Interfaces;
using Infrastructure.Common;
using Domain.Roots.Statuses;
using Domain.Roots.Statuses.Repos;

namespace Infrastructure.Persistence.Repos;

public class StatusRepository : BookingRepositoryBasic<Status>, IStatusRepository
{
    public StatusRepository(IBookingDbContext bookingDbContext) : base(bookingDbContext)
    {
    }
}

