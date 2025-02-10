using Application.Room.Dtos;
using Application.Room.Ports;
using Application.Room.Requests;
using Application.Room.Responses;
using Domain.Room.Exceptions;
using Domain.Room.Ports;

namespace Application.Room;
public class RoomManager(IRoomRepository roomRepository) : IRoomManager
{
    private readonly IRoomRepository _roomRepository = roomRepository;

    public async Task<RoomResponse> CreateRoom(CreateRoomRequest request)
    {
        try
        {
            var room = RoomDto.MapToEntity(request.Data);

            await room.Save(_roomRepository);
            request.Data.Id = room.Id;

            return new RoomResponse
            {
                Success = true,
                Data = request.Data,
            };
        }
        catch (InvalidRoomDataException)
        {

            return new RoomResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                Message = "Missing required information passed"
            };
        }
        catch (Exception)
        {
            return new RoomResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                Message = "There was an error when saving to DB"
            };
        }
    }

    public async Task<RoomResponse> GetRoom(int roomId)
    {
        var room = await _roomRepository.Get(roomId);

        if (room is null)
            return new RoomResponse { Success = false, ErrorCode = ErrorCodes.NOT_FOUND };

        return new RoomResponse
        {
            Success = true,
            Data = RoomDto.MapToDto(room),
        };
    }
}
