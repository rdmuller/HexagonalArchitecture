using Application.Room.Dtos;

namespace Application.Room.Requests;
public class CreateRoomRequest
{
    public required RoomDto Data { get; set; }
}
