using ApiApplication.Bookings.Guests;
using ApiApplication.DTOs;
using ApiApplication.Queries.Guests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooklyAPI.Controllers;

[Authorize]
[Tags("Guest")]
public class GuestController : BasicApiController
{
    [HttpGet]
    public async Task<ActionResult<List<GuestDto>>> GetGuests([FromQuery] string? searchQuery)
    {
        return await Mediator.Send(new GetGuestsQuery(searchQuery));
    }

    [HttpPost]
    public async Task<ActionResult> CreateGuest(CreateGuestCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateGuest(UpdateGuestCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGuest(int id)
    {
        await Mediator.Send(new DeleteGuestCommand(id));
        return NoContent();
    }
}