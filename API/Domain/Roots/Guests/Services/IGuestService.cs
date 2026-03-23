namespace Domain.Roots.Guests.Services;

public interface IGuestService
{
    Task<List<Guest>> GetAllGuests();

    Task<List<Guest>> GetFilteredGuests(string? searchQuery, bool includeBookings = false);

    Task<Guest?> GetGuestByNameAndSurname(string name, string surname);

    Task DeleteGuestById(int id);

    Task AddGuest(Guest guest);

    Task<Guest?> GetGuestById(int id);

    Task UpdateGuest(int id, string name, string surname);
}
