using Microsoft.AspNetCore.Mvc;
using Application;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Guest.Responses;
using Application.Guest.Dtos;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GuestController : ControllerBase
{
    private readonly ILogger<GuestController> _logger;
    private readonly IGuestManager _guestManager;

    public GuestController(ILogger<GuestController> logger, IGuestManager guestManager)
    {
        _logger = logger;
        _guestManager = guestManager;
    }

    [HttpPost]
    public async Task<IActionResult> Post(GuestDto guestDTO)
    {
        var request = new CreateGuestRequest { Data = guestDTO };

        var result = await _guestManager.CreateGuest(request);

        if (result.Success)
            return Created("", result.Data);

        if (Enum.IsDefined(typeof(ErrorCodes), result.ErrorCode!))
            return BadRequest(result);

        _logger.LogError(message: "Response with unknown ErrorCode returned", result);
        return BadRequest(500);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GuestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get(int guestId)
    {
        var res = await _guestManager.GetGuest(guestId);

        if (!res.Success)
            return NoContent();

        return Ok(res);
    }
}
