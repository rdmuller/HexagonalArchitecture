using Application.Guest.Dtos;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Guest.Responses;
using Domain.Exceptions;
using Domain.Guest.Exceptions;
using Domain.Guest.Ports;

namespace Application.Guest;
public class GuestManager(IGuestRepository guestRepository) : IGuestManager
{
    private readonly IGuestRepository _guestRepository = guestRepository;

    public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
    {
        try
        {
            var guest = GuestDto.MapToEntity(request.Data);

            await guest.Save(_guestRepository);

            request.Data.Id = guest.Id;

            return new GuestResponse
            {
                Data = request.Data,
                Success = true,
            };

        }
        catch (InvalidPersonDocumentIdException)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.INVALID_PERSON_ID,
                Message = "The document ID is not valid"
            };
        }
        catch (InvalidEmailException)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.INVALID_EMAIL,
                Message = "Invalid e-mail"
            };
        }
        catch (MissingRequiredInformationException)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                Message = "Missing required information"
            };
        }
        catch (Exception)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                Message = "There was an error when saving to DB"
            };
        }
    }

    public async Task<GuestResponse> GetGuest(int guestId)
    {
        var guest = await _guestRepository.Get(guestId);

        if (guest is null)
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.NOT_FOUND,
                Message = "No guest record was found with the given id"
            };

        return new GuestResponse
        {
            Success = true,
            Data = GuestDto.MapToDto(guest)
        };
    }
}
