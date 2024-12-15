using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.APPLICATION.Services.Contacts;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.CORE.DOMAIN.Models;
using FIAP.TECH.TESTS.TestDoubles.Data;
using FIAP.TECH.TESTS.TestDoubles.Fakes;
using FluentValidation;
using MassTransit;
using Moq;
using System.Linq.Expressions;

namespace FIAP.TECH.TESTS;

public class ContactServiceTests : BaseServiceTests<Contact>
{
    private readonly FakeContactRepository _fakeContactRepository;
    private readonly FakeRegionRepository _fakeRegionRepository;
    private readonly Mock<IBusControl> _busControlMock;
    private readonly Mock<IRequestClient<ContactByDDD>> _requestClientMock;
    private readonly ContactService _contactService;

    public ContactServiceTests()
    {
        _entityData = ContactData.Get();
        _fakeContactRepository = new FakeContactRepository(_entityData);
        _fakeRegionRepository = new FakeRegionRepository(RegionData.Get());
        _busControlMock = new Mock<IBusControl>();
        _requestClientMock = new Mock<IRequestClient<ContactByDDD>>();

        _contactService = new ContactService(
            _mapper, 
            _fakeRegionRepository, 
            _fakeContactRepository, 
            _busControlMock!.Object, 
            _requestClientMock!.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnListOfContactDto_WhenContactsAreFound()
    {
        // Arrange
        var contactDtos = new List<ContactDto>
        {
            new ContactDto { Id = 1, Name = "Contact 1", DDD = "11", Email = "", PhoneNumber = "" },
            new ContactDto { Id = 2, Name = "Contact 2", DDD = "11", Email = "", PhoneNumber = "" }
        };

        // Act
        var result = await _contactService.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count());

        // Verifica se os itens são equivalentes
        Assert.Equal(contactDtos[0].Id, result.ElementAt(0).Id);
        Assert.Equal(contactDtos[0].Name, result.ElementAt(0).Name);
        Assert.Equal(contactDtos[0].DDD, result.ElementAt(0).DDD);

        Assert.Equal(contactDtos[1].Id, result.ElementAt(1).Id);
        Assert.Equal(contactDtos[1].Name, result.ElementAt(1).Name);
        Assert.Equal(contactDtos[1].DDD, result.ElementAt(1).DDD);
    }

    [Fact]
    public async Task SendMessageAsync_ShouldCallPublish_WithCorrectContact()
    {
        var contact = new Contact
        {
            Name = "Contact 1",
            PhoneNumber = "123456789",
            Email = "contact1@email.com",
            DDD = "11"
        };

        // Act
        await _contactService.SendMessageAsync(contact);

        // Assert
        _busControlMock.Verify(
            x => x.Publish(contact, It.IsAny<CancellationToken>()),
            Times.Once,
            "O método Publish deve ser chamado uma vez com o contato correto."
        );
    }

    [Fact]
    public async Task GetById_ShouldReturnContactDto_WhenContactExists()
    {
        // Arrange
        var contactDto = new ContactDto
        {
            Id = 1,
            Name = "Contact 1",
            DDD = "11",
            Email = "",
            PhoneNumber = ""
        };

        // Act
        var result = await _contactService.GetById(1);

        // Assert
        Assert.NotNull(result); 
        Assert.Equal(contactDto.Id, result.Id);
        Assert.Equal(contactDto.Name, result.Name);
        Assert.Equal(contactDto.DDD, result.DDD);
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenContactDoesNotExist()
    {
        // Arrange
        int contactId = 99;

        // Act
        var result = await _contactService.GetById(contactId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByDdd_ShouldReturnListOfContactDetailsDto_WhenContactsExistForGivenDdd()
    {
        // Arrange
        var contactDetailsDtos = new List<ContactDetailsDto>
        {
            new() { Id = 1, Name = "Contact 1", DDD = "11" },
            new() { Id = 2, Name = "Contact 2", DDD = "11" }
        };

        // Act
        var result = await _contactService.GetByDdd("11");

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(contactDetailsDtos[0].Id, result.ElementAt(0).Id);
        Assert.Equal(contactDetailsDtos[1].Id, result.ElementAt(1).Id);
    }

    [Fact]
    public async Task GetByDdd_ShouldReturnEmptyList_WhenNoContactsExistForGivenDdd()
    {
        // Arrange
        string ddd = "99";

        // Act
        var result = await _contactService.GetByDdd(ddd);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task Update_ShouldThrowValidationException_WhenDDDIsInvalid()
    {
        // Arrange
        var contact = new Contact { Id = 1, DDD = "99", Email = "", Name = "", PhoneNumber = "" };

        var contactUpdateDto = new ContactUpdateDto { Id = 1, DDD = "99" };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() => _contactService.Update(contactUpdateDto));
        Assert.Equal("DDD inválido.", exception.Message);
    }

    [Fact]
    public async Task Delete_ShouldThrowInvalidOperationException_WhenContactDoesNotExist()
    {
        // Arrange
        int contactId = 99;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _contactService.Delete(contactId));
        Assert.Equal("Contato com o ID informado não existe.", exception.Message);
    }

    [Fact]
    public async Task SendResponseMessageAsync_ShouldThrowException_WhenRequestFails()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();
        var regionRepositoryMock = new Mock<IRegionRepository>();
        var contactRepositoryMock = new Mock<IContactRepository>();
        var busControlMock = new Mock<IBusControl>();
        var requestClientMock = new Mock<IRequestClient<ContactByDDD>>();

        var ddd = new ContactByDDD("11");

        requestClientMock
            .Setup(x => x.GetResponse<ContactResponse>(ddd, default, default)) // Simula a falha
            .ThrowsAsync(new Exception("Erro ao solicitar a resposta"));

        var contactService = new ContactService(
            mapperMock.Object,
            regionRepositoryMock.Object,
            contactRepositoryMock.Object,
            busControlMock.Object,
            requestClientMock.Object
        );

        // Act & Assert
        await Assert.ThrowsAsync<System.Exception>(() => contactService.SendResponseMessageAsync(ddd));
    }

}
