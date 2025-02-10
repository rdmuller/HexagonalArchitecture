using Domain.Exceptions;
using Domain.Guest.Exceptions;
using Domain.Guest.Ports;
using Domain.Guest.ValueObjects;

namespace Domain.Guest.Entities;
public class Guest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public PersonId? DocumentId { get; set; }

    public bool IsValid()
    {
        try
        {
            this.ValidateState();
            return true;
        } 
        catch (Exception)
        {
            return false;
        }
    }

    private void ValidateState()
    {
        if (DocumentId is null || string.IsNullOrWhiteSpace(DocumentId.IdNumber) || DocumentId.IdNumber.Length < 4 || DocumentId.PersonIdType == 0)
            throw new InvalidPersonDocumentIdException();

        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) || string.IsNullOrWhiteSpace(Email))
            throw new MissingRequiredInformationException();

        if (!Utils.ValidateEmail(Email))
            throw new InvalidEmailException();
    }

    public async Task Save(IGuestRepository guestRepository)
    {
        ValidateState();

        if (Id == 0)
            Id = await guestRepository.Create(this);
        //else
        //await guestRepository.Update(this);
    }
}
