using Domain.Roots.Accounts.Services;
using MediatR;

namespace ApiApplication.Queries.Account;

public class GetIsUserNameTakenQuery : IRequest<bool>
{
    public string UserName { get; set; } = null!;

    public GetIsUserNameTakenQuery(string userName) => UserName = userName;
}

public class GetIsUserNameTakenQueryHandler : IRequestHandler<GetIsUserNameTakenQuery, bool>
{
    private readonly IAccountService _accountService;

    public GetIsUserNameTakenQueryHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<bool> Handle(GetIsUserNameTakenQuery request, CancellationToken cancellationToken)
    {
        var user = await _accountService.GetAccountByUsername(request.UserName);

        return user != null;
    }
}