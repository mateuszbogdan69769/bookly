using ApiApplication.DTOs;
using Domain.Roots.Guests.Services;
using MediatR;

namespace ApiApplication.Queries.Guests;

public class GetGuestsQuery : IRequest<List<GuestDto>>
{
    public string? SearchQuery { get; set; }
    public GetGuestsQuery(string? searchQuery) => SearchQuery = searchQuery;
}

public class GetGuestsQueryHandler : IRequestHandler<GetGuestsQuery, List<GuestDto>>
{
    private readonly IGuestService _guestService;

    public GetGuestsQueryHandler(IGuestService guestService)
    {
        _guestService = guestService;
    }

    public async Task<List<GuestDto>> Handle(GetGuestsQuery request, CancellationToken cancellationToken)
    {
        var result = await _guestService.GetFilteredGuests(request.SearchQuery, includeBookings: true);

        return result.Select(x => GuestDto.Map(x, mapBookings: true)).ToList();
    }
}