using Ardalis.Specification;

namespace Domain.Roots.Guests.Specyfications;

public class GuestsByNameAndSurnameSpec : Specification<Guest>
{
    public GuestsByNameAndSurnameSpec(string name, string surname)
    {
        Query.Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower());
        Query.Where(x => x.Surname.Trim().ToLower() == surname.Trim().ToLower());
    }
}
