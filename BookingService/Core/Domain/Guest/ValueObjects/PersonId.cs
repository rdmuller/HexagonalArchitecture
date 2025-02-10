using Domain.Guest.Enums;

namespace Domain.Guest.ValueObjects;
public class PersonId
{
    public string IdNumber { get; set; } = string.Empty;
    public DocumentType PersonIdType { get; set; }
}
