using Domain.Roots.Accounts.Repos;
using Domain.Roots.Accounts;
using Domain.Interfaces;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Repos;

public class AccountRepository : BookingRepositoryBasic<Account>, IAccountRepository
{
    public AccountRepository(IBookingDbContext bookingDbContext) : base(bookingDbContext)
    {
    }
}

