using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooklyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BasicApiController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}

