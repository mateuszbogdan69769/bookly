using System.ComponentModel.DataAnnotations;
using Domain.Roots.Statuses.Repos;
using Domain.Roots.Statuses.Specyfications;

namespace Domain.Roots.Statuses.Services;

public class StatusService : IStatusService
{
    private readonly IStatusRepository _statusRepository;

    public StatusService(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public async Task AddStatus(Status status)
    {
        var maxOrder = await GetMaxOrder();
        status.SetOrder(maxOrder + 1);
        await _statusRepository.AddAsync(status);
    }

    public async Task DeleteStatusById(int id)
    {
        var statusToDelete = await GetStatusById(id, includeBookings: true);

        if (statusToDelete == null)
        {
            throw new ValidationException($"Status z id {id} nie istnieje");
        }

        if (statusToDelete.Bookings.Count > 0)
        {
            statusToDelete.Archive();
            await _statusRepository.UpdateAsync(statusToDelete);
        }
        else
        {
            await _statusRepository.DeleteAsync(statusToDelete);
        }
    }

    public async Task<List<Status>> GetAllStatuses(bool includeBookings = false)
    {
        var spec = new StatusSpec(includeBookings: includeBookings);
        return await _statusRepository.ListAsync(spec);
    }

    public async Task UpdateStatus(int id, string name, string color)
    {
        var status = await GetStatusById(id);

        if (status == null)
        {
            throw new ValidationException($"Status z id {id} nie istnieje");
        }

        status.Update(name, color);

        await _statusRepository.UpdateAsync(status);
    }

    public async Task UpdateStatusesOrder(List<(int id, int order)> values)
    {
        var allStatuses = await GetAllStatuses();

        foreach (var value in values)
        {
            var status = allStatuses.FirstOrDefault(x => x.Id == value.id);
            if (status == null)
            {
                throw new ValidationException("Status nie znaleziony");
            }

            status.SetOrder(value.order);
        }

        await _statusRepository.UpdateRangeAsync(allStatuses);
    }

    private async Task<Status?> GetStatusById(int id, bool includeBookings = false)
    {
        var spec = new StatusByIdSpec(id, includeBookings: includeBookings);
        return await _statusRepository.FirstOrDefaultAsync(spec);
    }

    private async Task<int> GetMaxOrder()
    {
        var allStatuses = await GetAllStatuses();
        if (allStatuses.Count == 0)
        {
            return 0;
        }

        return allStatuses.Max(x => x.Order);
    }
}