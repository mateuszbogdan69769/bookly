using ApiApplication.Queries.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooklyAPI.Controllers;

[Authorize]
[Tags("Statistic")]
public class StatisticController : BasicApiController
{
    [HttpGet]
    public async Task<ActionResult<StatsModel>> GetStats()
    {
        return await Mediator.Send(new GetStatisticsQuery());
    }
}