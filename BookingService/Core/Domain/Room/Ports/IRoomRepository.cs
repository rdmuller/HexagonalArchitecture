using Domain.Room.Entities;

namespace Domain.Room.Ports;
public interface IRoomRepository
{
    Task<Entities.Room> Get(int id);
    Task<Entities.Room> GetAgregate(int id);
    Task<int> Create(Entities.Room? room);
}
