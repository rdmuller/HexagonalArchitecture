using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;
public class GuestRepository(HotelDbContext hotelDbContext) : IGuestRepository
{
    private readonly HotelDbContext _hotelDbContext = hotelDbContext;

    public async Task<int> Create(Guest guest)
    {
        await _hotelDbContext.Guests.AddAsync(guest);
        await _hotelDbContext.SaveChangesAsync();

        return guest.Id;
    }

    public async Task<Guest?> Get(int id)
    {
        return await _hotelDbContext.Guests.AsNoTracking().FirstOrDefaultAsync(g => g.Id.Equals(id));
    }
}
