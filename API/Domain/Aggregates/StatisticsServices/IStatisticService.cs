using Domain.Aggregates.StatisticsServices.Models;

namespace Domain.Aggregates.StatisticsServices;

public interface IStatisticService
{
    Task<DashboardStats> GetStats();
}
