using Domain.Roots.Guests.Repos;
using Domain.Roots.Guests.Specyfications;
using System.ComponentModel.DataAnnotations;

namespace Domain.Roots.Guests.Services;

public class GuestService : IGuestService
{
    private readonly IGuestRepository _guestRepository;

    public GuestService(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<List<Guest>> GetAllGuests()
    {
        return await _guestRepository.ListAsync();
    }

    public async Task<List<Guest>> GetFilteredGuests(string? searchQuery, bool includeBookings = false)
    {
        var spec = new GuestsFilteredSpec(searchQuery, includeBookings);
        return await _guestRepository.ListAsync(spec);
    }

    public async Task<Guest?> GetGuestByNameAndSurname(string name, string surname)
    {
        var spec = new GuestsByNameAndSurnameSpec(name, surname);
        return await _guestRepository.FirstOrDefaultAsync(spec);
    }

    public async Task DeleteGuestById(int id)
    {
        var guest = await GetGuestById(id);
        if (guest is null)
        {
            throw new ValidationException("Gość nie znaleziony");
        }
        await _guestRepository.DeleteAsync(guest);
    }

    public async Task AddGuest(Guest guest)
    {
        await _guestRepository.AddAsync(guest);
    }

    public async Task<Guest?> GetGuestById(int id)
    {
        var spec = new GuestByIdSpec(id);
        return await _guestRepository.FirstOrDefaultAsync(spec);
    }

    public async Task UpdateGuest(int id, string name, string surname)
    {
        var guest = await GetGuestById(id);
        if (guest is null)
        {
            throw new ValidationException("Gość nie znaleziony");
        }
        guest.Update(name, surname);
        await _guestRepository.UpdateAsync(guest);
    }
}
