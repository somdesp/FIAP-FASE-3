using FIAP.TECH.CORE.DomainObjects;
using FIAP.TECH.CORE.DTOs;

namespace FIAP.TECH.API.CREATION.Services
{
    public interface ICreationService
    {
        Task<ContactResponse> SendMessageAsync(Contact message);
    }
}
