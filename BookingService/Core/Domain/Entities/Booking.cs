using Domain.Enums;
using Action = Domain.Enums.Action;

namespace Domain.Entities;
public class Booking
{
    public int id { get; set; }
    public DateTimeOffset PlacedAt { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    private Status Status { get; set; } = Status.Created;
    public Room Room { get; set; } 
    public Guest Guest { get; set; }

    public Status CurrentStatus { get { return this.Status; } }

    public void ChangeStatus(Action action)
    {
        this.Status = (this.Status, action) switch
        {
            (Status.Created, Action.Pay)     => Status.Paid,
            (Status.Created, Action.Cancel)  => Status.Canceled,
            (Status.Paid, Action.Finish)     => Status.Finished,
            (Status.Paid, Action.Refound)    => Status.Refounded,
            (Status.Canceled, Action.Reopen) => Status.Created,
            _ => this.Status,
        };
    }
}
