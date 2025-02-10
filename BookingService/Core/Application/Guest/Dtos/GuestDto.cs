using Domain.Guest.Enums;
using Domain.Guest.ValueObjects;

namespace Application.Guest.Dtos;
public class GuestDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string IdNumber { get; set; } = string.Empty;
    public int IdTypeCode { get; set; }

    public static Domain.Guest.Entities.Guest MapToEntity(GuestDto guestDTO)
    {
        return new Domain.Guest.Entities.Guest
        {
            Id = guestDTO.Id,
            Name = guestDTO.Name,
            Surname = guestDTO.Surname,
            Email = guestDTO.Email,
            DocumentId = new PersonId
            {
                IdNumber = guestDTO.IdNumber,
                PersonIdType = (DocumentType)guestDTO.IdTypeCode
            }
        };
    }

    public static GuestDto MapToDto(Domain.Guest.Entities.Guest guest)
    {
        return new GuestDto
        {
            Id = guest.Id,
            Name = guest.Name,
            Surname = guest.Surname,
            Email = guest.Email,
            IdNumber = guest.DocumentId!.IdNumber,
            IdTypeCode = (int)guest.DocumentId.PersonIdType
        };
    }
}
