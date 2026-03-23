using ApiApplication.DTOs;
using Domain.Roots.Bookings.Services;
using MediatR;

namespace ApiApplication.Queries.Bookings;

public class GetBookingsQuery : IRequest<List<BookingDto>>
{
    public string? SearchQuery { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}

public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, List<BookingDto>>
{
    private readonly IBookingService _bookingService;

    public GetBookingsQueryHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<List<BookingDto>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        var result = await _bookingService.GetFilteredBookings(request.SearchQuery, request.DateFrom, request.DateTo);

        return result.Select(BookingDto.Map).ToList();
    }
}