using Application.Booking.Dtos;
using Application.Booking.Responses;
using AutoMapper;
using Domain.Booking.Ports;
using MediatR;

namespace Application.Booking.Queries.GetById;

public class GetByIdBookingHandler(
    IBookingRepository bookingRepository, 
    IMapper mapper) : IRequestHandler<GetByIdBookingQuery, BookingResponse>
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BookingResponse> Handle(GetByIdBookingQuery request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.Get(request.Id);

        if (booking is null)
            return new BookingResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.NOT_FOUND,
                Message = "Booking not found"
            };

        var bookingDto = _mapper.Map<BookingDto>(booking);
        return new BookingResponse
        {
            Data = bookingDto,
            Success = true,
        };
    }
}
