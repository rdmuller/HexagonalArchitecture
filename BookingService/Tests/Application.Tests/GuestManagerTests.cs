using Application.Guest;
using Application.Guest.Dtos;
using Application.Guest.Requests;
using Domain.Guest.Enums;
using Domain.Guest.Ports;
using Moq;

namespace Application.Tests;
public class GuestManagerTests
{
    GuestManager guestManager;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task HappyPath()
    {
        var guestDto = new GuestDto
        {
            Name = "Fulano",
            Surname = "Ciclano",
            Email = "abc@gmail.com",
            IdNumber = "abca",
            IdTypeCode = 1
        };

        var expectedId = 222;

        var request = new CreateGuestRequest()
        {
            Data = guestDto,
        };

        var fakeRepo = new Mock<IGuestRepository>();

        fakeRepo.Setup(x => x.Create(
            It.IsAny<Domain.Guest.Entities.Guest>())).Returns(Task.FromResult(expectedId));

        guestManager = new GuestManager(fakeRepo.Object);

        var res = await guestManager.CreateGuest(request);
        Assert.That(res, Is.Not.Null);
        Assert.That(res.Success, Is.True);
        Assert.That(expectedId, Is.EqualTo(res.Data!.Id));
        Assert.That(guestDto.Name, Is.EqualTo(res.Data!.Name));
    }

    [TestCase("")]
    [TestCase("a")]
    [TestCase("ab")]
    [TestCase("abc")]
    [TestCase(null)]
    public async Task Should_Return_InvalidPersonDocumentIdException_WhenDocsAreInvalid(string? docNumber)
    {
        var guestDto = new GuestDto
        {
            Name = "Fulano",
            Surname = "Ciclano",
            Email = "abc@gmail.com",
            IdNumber = docNumber,
            IdTypeCode = 1
        };

        var request = new CreateGuestRequest()
        {
            Data = guestDto,
        };

        var fakeRepo = new Mock<IGuestRepository>();

        fakeRepo.Setup(x => x.Create(
            It.IsAny<Domain.Guest.Entities.Guest>())).Returns(Task.FromResult(222));

        guestManager = new GuestManager(fakeRepo.Object);

        var res = await guestManager.CreateGuest(request);

        Assert.IsNotNull(res);
        Assert.That(res.Success, Is.False);
        Assert.That(ErrorCodes.INVALID_PERSON_ID, Is.EqualTo(res.ErrorCode));
        Assert.That("The document ID is not valid", Is.EqualTo(res.Message));
    }

    [TestCase("", "surnametest", "asdf@gmail.com")]
    [TestCase(null, "surnametest", "asdf@gmail.com")]
    [TestCase("Fulano", "", "asdf@gmail.com")]
    [TestCase("Fulano", null, "asdf@gmail.com")]
    [TestCase("Fulano", "surnametest", "")]
    [TestCase("Fulano", "surnametest", null)]
    public async Task Should_Return_MissingRequiredInformation_WhenDocsAreInvalid(string? name, string? surname, string? email)
    {
        var guestDto = new GuestDto
        {
            Name = name,
            Surname = surname,
            Email = email,
            IdNumber = "abcd",
            IdTypeCode = 1
        };

        var request = new CreateGuestRequest()
        {
            Data = guestDto,
        };

        var fakeRepo = new Mock<IGuestRepository>();

        fakeRepo.Setup(x => x.Create(
            It.IsAny<Domain.Guest.Entities.Guest>())).Returns(Task.FromResult(222));

        guestManager = new GuestManager(fakeRepo.Object);

        var res = await guestManager.CreateGuest(request);

        Assert.IsNotNull(res);
        Assert.False(res.Success);
        Assert.That(ErrorCodes.MISSING_REQUIRED_INFORMATION, Is.EqualTo(res.ErrorCode));
        Assert.That("Missing required information", Is.EqualTo(res.Message));
    }

    [Test]
    public async Task Should_Return_GuestNotFound_When_GuestDoesntExist()
    {
        var fakeRepo = new Mock<IGuestRepository>();

        fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Domain.Guest.Entities.Guest?>(null));

        guestManager = new GuestManager(fakeRepo.Object);

        var res = await guestManager.GetGuest(333);

        Assert.IsNotNull(res);
        Assert.False(res.Success);
        Assert.That(ErrorCodes.NOT_FOUND, Is.EqualTo(res.ErrorCode));
    }

    [Test]
    public async Task Should_Return_Guest_Success()
    {
        var fakeRepo = new Mock<IGuestRepository>();

        var fakeGuest = new Domain.Guest.Entities.Guest
        {
            Id = 333,
            Name = "Test",
            DocumentId = new Domain.Guest.ValueObjects.PersonId
            {
                PersonIdType = DocumentType.DriveLicence,
                IdNumber = "123"
            }
        };

        fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult((Domain.Guest.Entities.Guest?)fakeGuest));

        guestManager = new GuestManager(fakeRepo.Object);

        var res = await guestManager.GetGuest(333);

        Assert.IsNotNull(res);
        Assert.True(res.Success);
        Assert.That(fakeGuest.Id, Is.EqualTo(res.Data.Id));
        Assert.That(fakeGuest.Name, Is.EqualTo(res.Data.Name));
    }
}
