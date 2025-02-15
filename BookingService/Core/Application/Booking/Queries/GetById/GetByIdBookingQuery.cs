using Application.Booking.Responses;
using MediatR;

namespace Application.Booking.Queries.GetById;

public class GetByIdBookingQuery : IRequest<BookingResponse>
{
    public int Id { get; set; }
}
