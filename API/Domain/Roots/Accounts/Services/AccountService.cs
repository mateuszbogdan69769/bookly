using Domain.Roots.Accounts.Repos;
using Domain.Roots.Accounts.Specyfications;
using Microsoft.Extensions.Configuration;

namespace Domain.Roots.Accounts.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IConfiguration _configuration;

    public AccountService(IAccountRepository accountRepository, IConfiguration configuration)
    {
        _accountRepository = accountRepository;
        _configuration = configuration;
    }

    public async Task AddAccount(string externalId, string name, string userName, string password)
    {
        var newAccount = new Account(externalId, name, userName);
        await _accountRepository.AddAsync(newAccount);
    }

    public async Task<Account?> GetAccountByUsername(string userName)
    {
        var spec = new AccountByUsernameSpec(userName);
        return await _accountRepository.FirstOrDefaultAsync(spec);
    }
}
