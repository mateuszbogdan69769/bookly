namespace Domain.Roots.Accounts.Services;

public interface IAccountService
{
    Task<Account?> GetAccountByUsername(string userName);

    Task AddAccount(string externalId, string name, string username, string password);
}
