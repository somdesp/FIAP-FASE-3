using FIAP.TECH.CORE.APPLICATION.Authentication;
using FIAP.TECH.CORE.APPLICATION.Settings.JwtExtensions;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FIAP.TECH.CORE.APPLICATION.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly TokenSettings _tokenSettings;

    public UserService(IUserRepository userRepository, IOptions<TokenSettings> tokenSettings)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(_userRepository));
        _tokenSettings = tokenSettings.Value;
    }

    public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.Authenticate(request.Email, request.Password);

        if (user is null) return null;

        var token = await GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });

        return tokenHandler.WriteToken(token);
    }
}
