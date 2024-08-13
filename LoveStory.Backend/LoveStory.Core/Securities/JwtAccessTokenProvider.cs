using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LoveStory.Core.Securities;

public class JwtAccessTokenProvider : IAccessTokenProvider
{
    const string Issuer = "LoveStory.Auth.Issuer";
    const string Audience = "LoveStory.Web.Application";
    private const string SecretKey = "LoveStory.Credential.Authenticate.SecretKey@202407.CreateByMinions.BuildInTaiwan";

    public string GenerateAccessToken(AuthOriginLoginAccessTokenDto dto)
    {
        var claims = GenerateClaims(dto);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var token = new JwtSecurityToken(claims: claims, signingCredentials: credentials, expires: dto.Expired);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private List<Claim> GenerateClaims(AuthOriginLoginAccessTokenDto dto)
    {
        return
        [
            new Claim(JwtRegisteredClaimNames.Iss, Issuer),
            new Claim(JwtRegisteredClaimNames.Sub, dto.Username),
            new Claim(JwtRegisteredClaimNames.Aud, Audience),
            new Claim(JwtRegisteredClaimNames.Iat, dto.IssueAt.ToString(CultureInfo.CurrentCulture)),
            new Claim(JwtRegisteredClaimNames.Exp, dto.Expired.ToString(CultureInfo.CurrentCulture))
        ];
    }

    public bool ValidateAccessToken(string accessToken)
    {
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Issuer,
                ValidateAudience = true,
                ValidAudience = Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal =
                new JwtSecurityTokenHandler().ValidateToken(accessToken, validationParameters, out var validatedToken);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}