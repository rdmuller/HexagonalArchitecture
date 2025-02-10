using Domain.Booking.Enums;
using Domain.Booking.Ports;
using Domain.Exceptions;
using Domain.Guest.Exceptions;
using Domain.Room.Exceptions;
using System.Text.Json;
using Action = Domain.Guest.Enums.Action;

namespace Domain.Booking.Entities;
public class Booking
{
    public int Id { get; set; }
    public DateTimeOffset PlacedAt { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public Status Status { get; set; } = Status.Created;

    public int RoomId { get; set; }
    public virtual Room.Entities.Room? Room { get; set; }

    public int GuestId { get; set; }
    public virtual Guest.Entities.Guest? Guest { get; set; }

    public void ChangeStatus(Action action)
    {
        Status = (Status, action) switch
        {
            (Status.Created, Action.Pay) => Status.Paid,
            (Status.Created, Action.Cancel) => Status.Canceled,
            (Status.Paid, Action.Finish) => Status.Finished,
            (Status.Paid, Action.Refound) => Status.Refounded,
            (Status.Canceled, Action.Reopen) => Status.Created,
            _ => Status,
        };
    }

    public async Task Save(IBookingRepository bookingRepository)
    {
        this.ValidateState();

        var booking = JsonSerializer.Deserialize<Booking>(JsonSerializer.Serialize(this));
        booking.Room = null;
        booking.Guest = null;

        if (this.Id == 0)
        {
            var res = await bookingRepository.Create(booking);
            this.Id = res.Id;
        }
        else
        {
            // update
        }
    }

    private void ValidateState()
    {
        if (PlacedAt == default)
            throw new MissingRequiredInformationException();

        if (Guest is null || !Guest.IsValid())
            throw new GuestHasInvalidInformationException();

        if (Room is null)
            throw new MissingRequiredInformationException();

        if (!Room.CanBeBooked())
            throw new RoomCannotBeBookedException();
    }

}
