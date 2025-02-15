using Application;
using Application.Booking.Commands.Create;
using Application.Booking.Dtos;
using Application.Booking.Ports;
using Application.Booking.Queries.GetById;
using Application.Booking.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly ILogger<GuestController> _logger;
    private readonly IBookingManager _bookingManager;
    private readonly IMediator _mediator;

    public BookingController(IBookingManager bookingManager, ILogger<GuestController> logger, IMediator mediator)
    {
        _bookingManager = bookingManager;
        _logger = logger;
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<IActionResult> Post(BookingDto request)
    {
        var response = await _mediator.Send(new CreateBookingCommand { BookingDto = request });

        if (response.Success)
            return Created("", response);
        
        if (Enum.IsDefined(typeof(ErrorCodes), response.ErrorCode!))
            return BadRequest(response);

        _logger.LogError(message: "Response with unknown ErrorCode returned", response);
        return BadRequest(500);
    }

    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetByIdBookingQuery { Id = id });

        if (!response.Success || response.Data is null || response.Data.Id == 0)
            return NoContent();

        return Ok(response);
    }
}
