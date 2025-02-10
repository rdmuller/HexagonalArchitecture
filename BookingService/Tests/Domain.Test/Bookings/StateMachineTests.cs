using Domain.Booking.Enums;

namespace Domain.Tests.Bookings;
public class StateMachineTests
{
    [Test]
    public void ShouldAlwaysStartWithCreatedStatus()
    {
        var booking = new Domain.Booking.Entities.Booking();

        Assert.That(booking.Status, Is.EqualTo(Status.Created));
    }

    [Test]
    public void ShouldSetStatusToPaidWhenPayingForBookingWithCreatedStatus()
    {
        var booking = new Domain.Booking.Entities.Booking();

        booking.ChangeStatus(Guest.Enums.Action.Pay);

        Assert.That(booking.Status, Is.EqualTo(Status.Paid));
    }

    [Test]
    public void ShouldSetStatusToFinishedWhenFinishingAPaidBooking()
    {
        var booking = new Domain.Booking.Entities.Booking();

        booking.ChangeStatus(Guest.Enums.Action.Pay);
        booking.ChangeStatus(Guest.Enums.Action.Finish);

        Assert.That(booking.Status, Is.EqualTo(Status.Finished));
    }
}
