using Application;
using Application.Booking.Dtos;
using Application.Booking.Ports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly ILogger<GuestController> _logger;
    private readonly IBookingManager _bookingManager;

    public BookingController(IBookingManager bookingManager, ILogger<GuestController> logger)
    {
        _bookingManager = bookingManager;
        _logger = logger;
    }

    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<IActionResult> Post(BookingDto request)
    {
        var response = await _bookingManager.CreateBooking(request);

        if (response.Success)
            return Created("", response);
        
        if (Enum.IsDefined(typeof(ErrorCodes), response.ErrorCode!))
            return BadRequest(response);

        _logger.LogError(message: "Response with unknown ErrorCode returned", response);
        return BadRequest(500);
    }
}
