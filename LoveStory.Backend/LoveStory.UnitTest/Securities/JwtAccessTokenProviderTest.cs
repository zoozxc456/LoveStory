using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoveStory.Core.DTOs;
using LoveStory.Core.Securities;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;

namespace LoveStory.UnitTest.Securities;

[TestFixture]
public class JwtAccessTokenProviderTest
{
    const string Issuer = "LoveStory.Auth.Issuer";
    const string Audience = "LoveStory.Web.Application";
    const string SecretKey = "LoveStory.Credential.Authenticate.SecretKey@202407.CreateByMinions.BuildInTaiwan";

    private JwtAccessTokenProvider _provider;
    private List<Claim> _jwtSecurityClaims;
    private DateTime _issueDateTime, _expiredDateTime;
    private SymmetricSecurityKey _symmetricSecurityKey;
    private SigningCredentials _credentials;
    private JwtSecurityToken _jwtSecurityToken;
    private string _jwtTokenString;

    [SetUp]
    public void SetUp()
    {
        _provider = new JwtAccessTokenProvider();
        _jwtSecurityClaims = [];
        _jwtTokenString = string.Empty;
    }

    [Test]
    public void ValidateAccessToken_GivenValidAccessToken_ShouldReturnTrue()
    {
        SetValidAccessTokenScenario();
        var result = _provider.ValidateAccessToken(_jwtTokenString);

        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateAccessToken_GivenInvalidAccessToken_ShouldReturnFalse()
    {
        SetInvalidAccessTokenScenario();
        var result = _provider.ValidateAccessToken(_jwtTokenString);

        Assert.That(result, Is.False);
    }

    [Test]
    public void GenerateAccessToken_GivenValidCredentials_ShouldReturnCorrectToken()
    {
        var credentials = new AuthOriginLoginAccessTokenDto
        {
            Username = "admin",
            IssueAt = new DateTime(2024, 1, 1, 13, 00, 00),
            Expired = new DateTime(2024, 1, 1, 13, 30, 00)
        };

        var token = _provider.GenerateAccessToken(credentials);

        Assert.That(token, Is.EqualTo("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJMb3ZlU3RvcnkuQXV0aC5Jc3N1ZXIiLCJzdWIiOiJhZG1pbiIsImF1ZCI6IkxvdmVTdG9yeS5XZWIuQXBwbGljYXRpb24iLCJpYXQiOiIyMDI0LzEvMSAxOjAwOjAwIiwiZXhwIjoxNzA0MDg3MDAwfQ.VMwPerYFj7fojM7QOJm9dH8f7OElBVPUltdzfk-C7feIOn9roCxxXD1_1ZbFR-TCtbW39Q3_d4aUSQmeqot59w"));
    }

    private void SetValidAccessTokenScenario()
    {
        SetValidIssueDateTime();
        SetValidExpiredDateTime();
        SetJwtSecurityClaims();
        SetSymmetricSecurityKey();
        SetCredentials();
        SetJwtSecurityToken();
        SetJwtTokenString();
    }

    private void SetInvalidAccessTokenScenario()
    {
        SetInvalidIssueDateTime();
        SetInvalidExpiredDateTime();
        SetJwtSecurityClaims();
        SetSymmetricSecurityKey();
        SetCredentials();
        SetJwtSecurityToken();
        SetJwtTokenString();
    }

    private void SetJwtSecurityClaims()
    {
        _jwtSecurityClaims =
        [
            new Claim(JwtRegisteredClaimNames.Iss, Issuer),
            new Claim(JwtRegisteredClaimNames.Sub, "test_user"),
            new Claim(JwtRegisteredClaimNames.Aud, Audience),
            new Claim(JwtRegisteredClaimNames.Iat, _issueDateTime.ToString(CultureInfo.InvariantCulture)),
            new Claim(JwtRegisteredClaimNames.Exp, _expiredDateTime.ToString(CultureInfo.InvariantCulture))
        ];
    }

    private void SetValidIssueDateTime()
    {
        _issueDateTime = DateTime.Now;
    }

    private void SetValidExpiredDateTime()
    {
        _expiredDateTime = _issueDateTime.AddMinutes(30);
    }


    private void SetInvalidIssueDateTime()
    {
        _issueDateTime = DateTime.Parse("2024-01-01T00:00:00");
    }

    private void SetInvalidExpiredDateTime()
    {
        _issueDateTime = DateTime.Parse("2024-01-01T00:30:00");
    }

    private void SetSymmetricSecurityKey()
    {
        _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
    }

    private void SetJwtTokenString()
    {
        _jwtTokenString = new JwtSecurityTokenHandler().WriteToken(_jwtSecurityToken);
    }

    private void SetJwtSecurityToken()
    {
        _jwtSecurityToken = new JwtSecurityToken(claims: _jwtSecurityClaims,
            signingCredentials: _credentials,
            expires: _expiredDateTime);
    }

    private void SetCredentials()
    {
        _credentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha512);
    }
}