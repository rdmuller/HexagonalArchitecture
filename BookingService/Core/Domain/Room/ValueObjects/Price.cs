using Domain.Guest.Enums;

namespace Domain.Room.ValueObjects;
public class Price
{
    public decimal Value { get; set; }
    public AcceptedCurrencies Currency { get; set; }
}
