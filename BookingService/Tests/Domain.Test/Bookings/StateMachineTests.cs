using Domain.Entities;
using Domain.Enums;

namespace Domain.Tests.Bookings;
public class StateMachineTests
{
    [Test]
    public void ShouldAlwaysStartWithCreatedStatus()
    {
        var booking = new Booking();

        Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Created));
    }

    [Test]
    public void ShouldSetStatusToPaidWhenPayingForBookingWithCreatedStatus()
    {
        var booking = new Booking();

        booking.ChangeStatus(Enums.Action.Pay);

        Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Paid));
    }

    [Test]
    public void ShouldSetStatusToFinishedWhenFinishingAPaidBooking()
    {
        var booking = new Booking();

        booking.ChangeStatus(Enums.Action.Pay);
        booking.ChangeStatus(Enums.Action.Finish);

        Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Finished));
    }
}
