using Ardalis.Specification;

namespace Domain.Roots.Guests.Specyfications;

public class GuestByIdSpec : Specification<Guest>
{
    public GuestByIdSpec(int id)
    {
        Query.Where(x => x.Id == id);
    }
}
