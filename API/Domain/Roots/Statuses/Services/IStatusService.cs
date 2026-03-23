namespace Domain.Roots.Statuses.Services;

public interface IStatusService
{
    Task<List<Status>> GetAllStatuses(bool includeBookings = false);

    Task DeleteStatusById(int id);

    Task AddStatus(Status status);

    Task UpdateStatus(int id, string name, string color);

    Task UpdateStatusesOrder(List<(int id, int order)> values);
}