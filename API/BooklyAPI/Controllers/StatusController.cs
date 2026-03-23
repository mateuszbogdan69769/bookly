using ApiApplication.Commands.Statuses;
using ApiApplication.DTOs;
using ApiApplication.Queries.Statuses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooklyAPI.Controllers;

[Authorize]
[Tags("Status")]
public class StatusController : BasicApiController
{
    [HttpGet]
    public async Task<ActionResult<List<StatusDto>>> GetStatuses()
    {
        return await Mediator.Send(new GetStatusesQuery());
    }

    [HttpPost]
    public async Task<ActionResult> CreateStatus(CreateStatusCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateStatus(UpdateStatusCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPut("order")]
    public async Task<ActionResult> UpdateOrder(UpdateStatusOrderCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStatus(int id)
    {
        await Mediator.Send(new DeleteStatusCommand(id));
        return NoContent();
    }
}