using Domain.Booking.Entities;

namespace Domain.Booking.Ports;
public interface IBookingRepository
{
    Task<Entities.Booking> Create(Entities.Booking booking);
    Task<Entities.Booking> Get(int id);
}
