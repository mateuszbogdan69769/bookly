using Ardalis.Specification;

namespace Domain.Roots.Accounts.Specyfications;

public class AccountByUsernameSpec : Specification<Account>
{
    public AccountByUsernameSpec(string username)
    {
        Query.Where(x => x.Username == username);
    }
}
