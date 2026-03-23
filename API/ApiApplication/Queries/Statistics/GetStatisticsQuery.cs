using Domain.Aggregates.StatisticsServices;
using MediatR;

namespace ApiApplication.Queries.Statistics;

public class GetStatisticsQuery : IRequest<StatsModel>
{
}

public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, StatsModel>
{
    private readonly IStatisticService _statisticService;

    public GetStatisticsQueryHandler(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    public async Task<StatsModel> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
    {
        var result = await _statisticService.GetStats();

        return new StatsModel()
        {
            TotalBookings = result.TotalBookings,
            BookingsToday = result.BookingsToday,
            TotalGuests = result.TotalGuests,
            PeoplesToday = result.PeoplesToday
        };
    }
}


public class StatsModel
{
    public int TotalBookings { get; set; }
    public int BookingsToday { get; set; }
    public int TotalGuests { get; set; }
    public int PeoplesToday { get; set; }
}