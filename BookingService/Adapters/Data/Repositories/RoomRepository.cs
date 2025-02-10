using Domain.Room.Entities;
using Domain.Room.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;
public class RoomRepository(HotelDbContext dbContext) : IRoomRepository
{
    private readonly HotelDbContext _dbContext = dbContext;
    public async Task<int> Create(Room room)
    {
        _dbContext.Rooms.Add(room);
        await _dbContext.SaveChangesAsync();
        return room.Id;
    }

    public Task<Room?> Get(int Id)
    {
        return _dbContext.Rooms.AsNoTracking().Where(g => g.Id == Id).FirstOrDefaultAsync();
    }

    public Task<Room> GetAggregate(int Id)
    {
        return _dbContext.Rooms.AsNoTracking()
            .Include(r => r.Bookings)
            .Where(g => g.Id == Id).FirstAsync();
    }

    public Task<Room?> GetAgregate(int id)
    {
        return _dbContext.Rooms.AsNoTracking()
            .Include(r => r.Bookings)
            .FirstOrDefaultAsync(r => r.Id.Equals(id));
    }
}
