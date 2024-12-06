using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Models;

namespace FIAP.TECH.CORE.APPLICATION.Services.Contacts;

public interface IContactService
{
    Task SendMessageAsync(Contact message);
    Task Update(ContactUpdateDto contactDTO);
    Task Delete(int id);
    Task<IEnumerable<ContactDto>> GetAll();
    Task<ContactDto> GetById(int id);
    Task<IEnumerable<ContactDetailsDto>> GetByDdd(string ddd);
    Task<ContactResponse> SendResponseMessageAsync(ContactByDDD contactByDDD);
}
