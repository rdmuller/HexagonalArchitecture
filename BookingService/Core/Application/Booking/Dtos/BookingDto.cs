using Application.Guest.Dtos;
using Application.Room.Dtos;
using Domain.Booking.Entities;
using Domain.Booking.Enums;

namespace Application.Booking.Dtos;
public class BookingDto
{
    public int Id { get; set; }
    public DateTimeOffset PlacedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    private Status Status { get; set; } 
    public int RoomId { get; set; }
    public int GuestId { get; set; }

    public static Domain.Booking.Entities.Booking MapToEntity(BookingDto bookingDto)
    {
        return new Domain.Booking.Entities.Booking
        {
            Id = bookingDto.Id,
            Start = bookingDto.Start,
            End = bookingDto.End,
            PlacedAt = bookingDto.PlacedAt,
            GuestId = bookingDto.GuestId,
            RoomId = bookingDto.RoomId,
        };
    }
}
