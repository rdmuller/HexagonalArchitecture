using Application.Booking.Dtos;
using Application.Booking.Ports;
using Application.Booking.Responses;
using Domain.Booking.Ports;
using Domain.Exceptions;
using Domain.Guest.Exceptions;
using Domain.Guest.Ports;
using Domain.Room.Exceptions;
using Domain.Room.Ports;

namespace Application.Booking;
public class BookingManager(
    IBookingRepository bookingRepository,
    IRoomRepository roomRepository,
    IGuestRepository guestRepository) : IBookingManager
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IGuestRepository _guestRepository = guestRepository;

    public async Task<BookingResponse> CreateBooking(BookingDto bookingDto)
    {
        try
        {
            var booking = BookingDto.MapToEntity(bookingDto);
            booking.Guest = await _guestRepository.Get(bookingDto.GuestId);
            booking.Room = await _roomRepository.GetAgregate(bookingDto.RoomId);

            await booking.Save(_bookingRepository);

            bookingDto.Id = booking.Id;

            return new BookingResponse
            {
                Success = true,
                Data = bookingDto,
            };
        }
        catch (MissingRequiredInformationException)
        {
            return new BookingResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                Message = "Missing required information"
            };
        }
        catch (GuestHasInvalidInformationException)
        {
            return new BookingResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.INVALID_GUEST,
                Message = "Invalid guest"
            };
        }
        catch (RoomCannotBeBookedException)
        {
            return new BookingResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.ROOM_CANNOT_BE_BOOKED,
                Message = "Room cannot be booked"
            };
        }
        catch (Exception) 
        {
            return new BookingResponse
            {
                Success = false,
                Message = "Unknown error"
            };
        }
    }

    public Task<BookingDto> GetBooking(int id)
    {
        throw new NotImplementedException();
    }
}
