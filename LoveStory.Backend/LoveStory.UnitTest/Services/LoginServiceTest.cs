using System.Linq.Expressions;
using LoveStory.Core.DTOs;
using LoveStory.Core.Interfaces;
using LoveStory.Core.Services;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using NSubstitute;

namespace LoveStory.UnitTest.Services;

[TestFixture]
public class LoginServiceTest
{
    private LoginService _loginService;
    private IRepository<UserData> _userRepository;
    private IHashProvider _hashProvider;
    private IAccessTokenProvider _accessTokenProvider;
    private LoginRequestDto _credentials;
    private UserData _userData;

    [SetUp]
    public void SetUp()
    {
        _userRepository = Substitute.For<IRepository<UserData>>();
        _hashProvider = Substitute.For<IHashProvider>();
        _accessTokenProvider = Substitute.For<IAccessTokenProvider>();
        _loginService = new LoginService(_userRepository, _hashProvider, _accessTokenProvider);
    }

    [Test]
    public void Login_GivenNotExistUser_ShouldThrowNotFoundException()
    {
        SetNotExistUserScenario();
        Assert.ThrowsAsync<Exception>(() => _loginService.Login(_credentials));
    }


    [Test]
    public void Login_GivenValidCredentials_ShouldReturnTrue()
    {
        SetValidCredentialsScenario();

        var (isSuccess, token) = _loginService.Login(_credentials).Result;

        Assert.Multiple(() =>
        {
            Assert.That(isSuccess, Is.True);
            Assert.That(token,
                Is.EqualTo(
                    "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJMb3ZlU3RvcnkuQXV0aC5Jc3N1ZXIiLCJzdWIiOiJ0ZXN0X2V4aXN0X3VzZXIiLCJhdWQiOiJMb3ZlU3RvcnkuV2ViLkFwcGxpY2F0aW9uIiwiaWF0IjoiMDEvMDEvMjAyNCAxMzowMDowMCIsImV4cCI6IjAxLzAxLzIwMjQgMTM6MzA6MDAifQ.hhpzJBfOLejvzYDnDPixWc-D6cINOU2NqvrZ3qyyMqQEPvD6MLUgCT6QMWrNHQxhlyOjzBSLPQAUXcvt8Igk-g"));
        });
    }

    [Test]
    public void Login_GivenInValidPassword_ShouldReturnFalse()
    {
        SetInvalidPasswordCredentialsScenario();

        var (isSuccess, token) = _loginService.Login(_credentials).Result;
        Assert.Multiple(() =>
        {
            Assert.That(isSuccess, Is.False);
            Assert.That(token, Is.Null);
        });
    }

    private void SetNotExistUserScenario()
    {
        SetNotExistUserCredentials();
        _userRepository.GetOneAsync(Arg.Any<Expression<Func<UserData, bool>>>()).Returns(null as UserData);
    }

    private void SetValidCredentialsScenario()
    {
        SetValidCredentials();
        SetValidUserData();
        _accessTokenProvider.GenerateAccessToken(Arg.Any<AuthOriginLoginAccessTokenDto>()).Returns("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJMb3ZlU3RvcnkuQXV0aC5Jc3N1ZXIiLCJzdWIiOiJ0ZXN0X2V4aXN0X3VzZXIiLCJhdWQiOiJMb3ZlU3RvcnkuV2ViLkFwcGxpY2F0aW9uIiwiaWF0IjoiMDEvMDEvMjAyNCAxMzowMDowMCIsImV4cCI6IjAxLzAxLzIwMjQgMTM6MzA6MDAifQ.hhpzJBfOLejvzYDnDPixWc-D6cINOU2NqvrZ3qyyMqQEPvD6MLUgCT6QMWrNHQxhlyOjzBSLPQAUXcvt8Igk-g");
        _userRepository.GetOneAsync(Arg.Any<Expression<Func<UserData, bool>>>()).Returns(_userData);
        _hashProvider.VerifyPassword(_credentials.Password, _userData.Password).Returns(true);
    }

    private void SetValidUserData()
    {
        _userData = new UserData
        {
            Username = "test_exist_user",
            Salted = "HelloWorld123456",
            Password = "d50aa5030f7e5187f80425491e8ff4004af06bdac5b896dd38aaf21c2df6852a"
        };
    }

    private void SetNotExistUserCredentials()
    {
        _credentials = new LoginRequestDto { Username = "testuser", Password = "password" };
    }

    private void SetValidCredentials()
    {
        _credentials = new LoginRequestDto { Username = "test_exist_user", Password = "test_exist_user_password" };
    }

    private void SetInvalidPasswordCredentials()
    {
        _credentials = new LoginRequestDto { Username = "test_exist_user", Password = "test_invalid_user_password" };
    }

    private void SetInvalidPasswordCredentialsScenario()
    {
        SetInvalidPasswordCredentials();
        SetValidUserData();

        _userRepository.GetOneAsync(Arg.Any<Expression<Func<UserData, bool>>>()).Returns(_userData);
        _hashProvider.VerifyPassword(_credentials.Password, _userData.Password).Returns(false);
    }
}