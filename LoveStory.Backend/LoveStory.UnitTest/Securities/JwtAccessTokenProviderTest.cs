using System.IdentityModel.Tokens.Jwt;
using System.Text;
using LoveStory.Core.DTOs;
using LoveStory.Core.Securities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LoveStory.UnitTest.Securities;

[TestFixture]
public class JwtAccessTokenProviderTest
{
    const string Issuer = "Test.Application.Issuer";
    const string Audience = "Test.Application.Audience";
    const string SecretKey = "ksD0o3bFJwQqT+/b8LShXcXJOmSP/ZlD7Hw4bQjZODVZlsDo+V7G8vgtqB2QjT13";

    private JwtAccessTokenProvider _provider;
    private IDictionary<string, object> _jwtSecurityClaims;
    private DateTime _issueDateTime, _expiredDateTime, _notBeforeDateTime;
    private SymmetricSecurityKey _symmetricSecurityKey;
    private SigningCredentials _credentials;
    private JwtSecurityToken _jwtSecurityToken;
    private string _jwtTokenString;
    private IConfiguration _configuration;
    private SecurityTokenDescriptor _securityTokenDescriptor;

    [SetUp]
    public void SetUp()
    {
        _configuration = Substitute.For<IConfiguration>();
        _configuration.GetSection("JwtConfig:Issuer").Value.Returns(Issuer);
        _configuration.GetSection("JwtConfig:WebAudience").Value.Returns(Audience);
        _configuration.GetSection("JwtConfig:SecretKey").Value.Returns(SecretKey);
        _provider = new JwtAccessTokenProvider(_configuration);
        _jwtSecurityClaims = new Dictionary<string, object>();
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
            Expired = new DateTime(2024, 1, 1, 13, 30, 00),
            NotBefore = new DateTime(2024, 1, 1, 13, 00, 00)
        };

        var token = _provider.GenerateAccessToken(credentials);

        Assert.That(token,
            Is.EqualTo(
                "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJUZXN0LkFwcGxpY2F0aW9uLklzc3VlciIsInN1YiI6ImFkbWluIiwiYXVkIjoiVGVzdC5BcHBsaWNhdGlvbi5BdWRpZW5jZSIsImlhdCI6MTcwNDA4NTIwMCwiZXhwIjoxNzA0MDg3MDAwLCJuYmYiOjE3MDQwODUyMDB9.YOfUaq7-mlP7KafRba6d24TLYn09I7eKED6twHrfjR1IeZhqTiOACFxtkhBErsFWmENOAgESamsq1_gv4w30iQ"));
    }

    private void SetValidAccessTokenScenario()
    {
        SetValidIssueDateTime();
        SetValidExpiredDateTime();
        SetValidNotBeforeDateTime();
        SetJwtSecurityClaims();
        SetSymmetricSecurityKey();
        SetCredentials();
        SetSecurityTokenDescriptor();
        SetJwtTokenString();
    }

    private void SetInvalidAccessTokenScenario()
    {
        SetInvalidIssueDateTime();
        SetInvalidExpiredDateTime();
        SetInvalidNotBeforeDateTime();
        SetJwtSecurityClaims();
        SetSymmetricSecurityKey();
        SetCredentials();
        SetSecurityTokenDescriptor();
        SetJwtTokenString();
    }

    private void SetJwtSecurityClaims()
    {
        _jwtSecurityClaims = new Dictionary<string, object>
        {
            { JwtRegisteredClaimNames.Iss, Issuer },
            { JwtRegisteredClaimNames.Sub, "test_user" },
            { JwtRegisteredClaimNames.Aud, Audience },
            { JwtRegisteredClaimNames.Iat, _issueDateTime },
            { JwtRegisteredClaimNames.Exp, _expiredDateTime },
            { JwtRegisteredClaimNames.Nbf, _notBeforeDateTime }
        };
    }

    private void SetValidIssueDateTime()
    {
        _issueDateTime = DateTime.Now;
    }

    private void SetValidExpiredDateTime()
    {
        _expiredDateTime = _issueDateTime.AddMinutes(30);
    }

    private void SetValidNotBeforeDateTime()
    {
        _notBeforeDateTime = _issueDateTime;
    }

    private void SetInvalidIssueDateTime()
    {
        _issueDateTime = DateTime.Parse("2024-01-01T00:00:00");
    }

    private void SetInvalidExpiredDateTime()
    {
        _issueDateTime = DateTime.Parse("2024-01-01T00:30:00");
    }

    private void SetInvalidNotBeforeDateTime()
    {
        _notBeforeDateTime = DateTime.Parse("2024-01-01T00:00:00");
    }

    private void SetSymmetricSecurityKey()
    {
        _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
    }

    private void SetJwtTokenString()
    {
        _jwtTokenString = new JsonWebTokenHandler().CreateToken(_securityTokenDescriptor);
    }

    private void SetSecurityTokenDescriptor()
    {
        _securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = _jwtSecurityClaims,
            SigningCredentials = _credentials,
            IssuedAt = _issueDateTime,
            Expires = _expiredDateTime,
            NotBefore = _notBeforeDateTime
        };
    }

    private void SetCredentials()
    {
        _credentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha512);
    }
}