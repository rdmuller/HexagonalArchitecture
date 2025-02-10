using Application.Booking.Dtos;
using Application.Booking.Responses;

namespace Application.Booking.Ports;
public interface IBookingManager
{
    Task<BookingResponse> CreateBooking(BookingDto bookingDto);
    Task<BookingDto> GetBooking(int id);
}
