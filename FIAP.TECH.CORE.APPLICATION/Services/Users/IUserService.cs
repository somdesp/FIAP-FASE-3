using FIAP.TECH.CORE.APPLICATION.Authentication;
using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.APPLICATION.Services.Users;

public interface IUserService
{
    Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request);
}
