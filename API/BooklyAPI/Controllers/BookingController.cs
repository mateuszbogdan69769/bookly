using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using ApiApplication.Bookings.Commands;
using ApiApplication.DTOs;
using ApiApplication.Queries.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooklyAPI.Controllers;

[Authorize]
[Tags("Booking")]
public class BookingController : BasicApiController
{
    [HttpGet]
    public async Task<ActionResult<List<BookingDto>>> GetBookings([FromQuery] string filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
        {
            throw new ValidationException("Filter jest pusty");
        }

        try
        {
            byte[] decodedBytes = Convert.FromBase64String(filter);
            string jsonString = Encoding.UTF8.GetString(decodedBytes);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var query = JsonSerializer.Deserialize<GetBookingsQuery>(jsonString, options);

            if (query == null)
            {
                throw new ValidationException("Nie udało się sparsować zapytania");
            }

            return await Mediator.Send(query);
        }
        catch (FormatException)
        {
            throw new ValidationException("Nieprawidłowy format base64");
        }
        catch (JsonException)
        {
            throw new ValidationException("Nieprawidłowy format JSON");
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateBooking(CreateBookingCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateBooking(UpdateBookingCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPut("status")]
    public async Task<ActionResult> UpdateBookingStatus(UpdateBookingStatusCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBooking(int id)
    {
        await Mediator.Send(new DeleteBookingCommand(id));
        return NoContent();
    }
}