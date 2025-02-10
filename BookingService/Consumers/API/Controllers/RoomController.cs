using Application.Room.Ports;
using Application;
using Microsoft.AspNetCore.Mvc;
using Application.Room.Requests;
using Application.Room.Dtos;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    private readonly IRoomManager _roomManager;

    public RoomController(ILogger<RoomController> logger, IRoomManager roomManager)
    {
        _logger = logger;
        _roomManager = roomManager;
    }

    [HttpPost]
    public async Task<ActionResult<RoomDto>> Post(RoomDto room)
    {
        /*var request = new CreateRoomCommand
        {
            RoomDto = room
        };

        var res = await _mediator.Send(request);*/

        var createRoom = new CreateRoomRequest { Data = room };

        var res = await _roomManager.CreateRoom(createRoom);

        if (res.Success) 
            return Created("", res.Data);
        else if (res.ErrorCode == ErrorCodes.MISSING_REQUIRED_INFORMATION)
            return BadRequest(res);
        else if (res.ErrorCode == ErrorCodes.COULD_NOT_STORE_DATA)
            return BadRequest(res);

        _logger.LogError("Response with unknown ErrorCode Returned", res);
        return BadRequest(500);
    }

    [HttpGet]
    public async Task<ActionResult<RoomDto>> Get(int roomId)
    {
        /*var query = new GetRoomQuery
        {
            Id = roomId
        };

        var res = await _mediator.Send(query);

        if (res.Success) return Ok(res.Data);
        */
        var res = await _roomManager.GetRoom(roomId);

        if (res.Success)
            return Ok(res);

        return NoContent();
    }
}
