using Domain.Enums;

namespace Domain.ValueObjects;
public class PersonId
{
    public string IdNumber { get; set; } = string.Empty;
    public DocumentType PersonIdType { get; set; }
}
