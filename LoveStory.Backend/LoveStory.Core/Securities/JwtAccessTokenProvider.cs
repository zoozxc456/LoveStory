using System.Text;
using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LoveStory.Core.Securities;

public class JwtAccessTokenProvider : IAccessTokenProvider
{
    private const string SecurityAlgorithmName = SecurityAlgorithms.HmacSha512;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _secretKey;

    public JwtAccessTokenProvider(IConfiguration configuration)
    {
        var jwtConfigSection = configuration.GetSection("JwtConfig");
        _issuer = configuration.GetSection("JwtConfig:Issuer").Value ?? string.Empty;
        _audience = configuration.GetSection("JwtConfig:WebAudience").Value ?? string.Empty;
        _secretKey = configuration.GetSection("JwtConfig:SecretKey").Value ?? string.Empty;
    }

    public string GenerateAccessToken(AuthOriginLoginAccessTokenDto dto)
    {
        var claims = GenerateClaims(dto);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithmName);
        return new JsonWebTokenHandler().CreateToken(new SecurityTokenDescriptor
        {
            Claims = claims,
            Audience = _audience,
            IssuedAt = dto.IssueAt,
            Expires = dto.Expired,
            NotBefore = dto.NotBefore,
            SigningCredentials = credentials
        });
    }

    private Dictionary<string, object> GenerateClaims(AuthOriginLoginAccessTokenDto dto)
    {
        return new Dictionary<string, object>
        {
            { JwtRegisteredClaimNames.Iss, _issuer },
            { JwtRegisteredClaimNames.Sub, dto.UserId.ToString() },
            { JwtRegisteredClaimNames.Aud, _audience },
            { JwtRegisteredClaimNames.Iat, dto.IssueAt },
            { JwtRegisteredClaimNames.Exp, dto.Expired },
            { JwtRegisteredClaimNames.Nbf, dto.NotBefore }
        };
    }

    public TokenValidationResult ValidateAccessToken(string accessToken)
    {
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            return new JsonWebTokenHandler().ValidateTokenAsync(accessToken, validationParameters).Result;
        }
        catch (Exception e)
        {
            throw new Exception(e.ToString());
        }
    }
}