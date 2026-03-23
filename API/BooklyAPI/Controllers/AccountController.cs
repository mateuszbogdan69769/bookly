using ApiApplication.Bookings.Commands;
using ApiApplication.Queries.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooklyAPI.Controllers;

[AllowAnonymous]
[Tags("Account")]
public class AccountController : BasicApiController
{
    [HttpGet("isUsernameTaken/{userName}")]
    public async Task<bool> GetIsUsernameTaken(string userName)
    {
        return await Mediator.Send(new GetIsUserNameTakenQuery(userName));
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthorizationData?>> Login(LoginCommand command)
    {
        return await Mediator.Send(command);
    }
}