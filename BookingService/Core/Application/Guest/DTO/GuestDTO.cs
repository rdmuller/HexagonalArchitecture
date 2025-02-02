using Domain.Enums;
using Domain.ValueObjects;
using Entities = Domain.Entities;

namespace Application.Guest.DTO;
public class GuestDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string IdNumber { get; set; } = string.Empty;
    public int IdTypeCode { get; set; }

    public static Entities.Guest MapToEntity(GuestDTO guestDTO)
    {
        return new Entities.Guest
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

    public static GuestDTO MapToDto(Entities.Guest guest)
    {
        return new GuestDTO
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
