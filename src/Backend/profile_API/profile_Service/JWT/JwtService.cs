using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using profile_Core.JWT;
using profile_Core.Model;
using profile_Core.Profile;

namespace profile_Service.JWT;

public class JwtService: IJwtService
{

    private readonly Auth _options;

    public JwtService(IOptions<Auth> options)
    {
        _options = options.Value;
    }
    public JwtModel GenerateJwtToken(Guid publicId, string login)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var nowUtc = DateTime.UtcNow;
        var expiresAt = nowUtc.AddHours(_options.AccessTokenLifetimeInHours);
        var tokenDescriptor = BuildSecurityTokenDescriptor(publicId, login, nowUtc, expiresAt);
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);

        return new JwtModel
        {
            ExpiredAt = expiresAt,
            Token = token
        };
    }

    public bool IsValidAuthToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var key = Encoding.ASCII.GetBytes(_options.EncryptionKey);
            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer,
                ValidateAudience = true,
                ValidAudience = _options.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            tokenHandler.ValidateToken(token, parameters, out _);
            return true;
        }
        catch
        {
            
            return false;
        }
    }
    private SecurityTokenDescriptor BuildSecurityTokenDescriptor(Guid publicId, string login, DateTime nowUtc, DateTime expires)
    {
        var key = Encoding.ASCII.GetBytes(_options.EncryptionKey);
        
        var claims = new[]
        {
            new Claim("Login", login),
            new Claim(ClaimTypes.Name, publicId.ToString()),
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            NotBefore = nowUtc,
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenDescriptor;
    }
}