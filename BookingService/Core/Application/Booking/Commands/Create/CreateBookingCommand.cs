using Application.Booking.Dtos;
using Application.Booking.Responses;
using MediatR;

namespace Application.Booking.Commands.Create;
public class CreateBookingCommand : IRequest<BookingResponse>
{
    public BookingDto? BookingDto { get; set; }
}
