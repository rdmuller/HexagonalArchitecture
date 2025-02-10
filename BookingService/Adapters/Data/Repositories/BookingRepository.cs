using Domain.Booking.Entities;
using Domain.Booking.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;
public class BookingRepository(HotelDbContext hotelDbContext) : IBookingRepository
{
    private readonly HotelDbContext _hotelDbContext = hotelDbContext;

    public async Task<Booking> Create(Booking booking)
    {
        await _hotelDbContext.Bookings.AddAsync(booking);
        await _hotelDbContext.SaveChangesAsync();

        return booking;
    }

    public async Task<Booking?> Get(int id)
    {
        return await _hotelDbContext.Bookings.AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id.Equals(id));
    }
}
