using Application.Booking.Dtos;
using AutoMapper;

namespace Application.Mapping;

public class BookingAutoMapping : Profile
{
    public BookingAutoMapping()
    {
        CreateMap<Domain.Booking.Entities.Booking, BookingDto>();
    }
}
