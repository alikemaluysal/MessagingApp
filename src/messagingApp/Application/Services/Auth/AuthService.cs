using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
    {
        _configuration = configuration;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public string CreateAccessToken(User user)
    {
        var audience = _configuration["TokenOptions:Audience"];
        var issuer = _configuration["TokenOptions:Issuer"];
        var accessTokenExpiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["TokenOptions:AccessTokenExpiration"]));
        var refreshTokenExpiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["TokenOptions:RefreshTokenExpiration"]));
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenOptions:SecurityKey"]));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Nickname),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer,
            audience,
            expires: accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            claims: claims,
            signingCredentials: signingCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var accessToken = tokenHandler.WriteToken(jwt);
        return accessToken;
    }

    public async Task<RefreshToken> CreateRefreshTokenAsync(User user, string ipAddress)
    {
        string refreshToken = Guid.NewGuid().ToString();
        var newRefreshToken = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["TokenOptions:RefreshTokenExpiration"])),
            CreatedByIp = ipAddress
        };

        await _refreshTokenRepository.AddAsync(newRefreshToken);
        return newRefreshToken;
    }
}
