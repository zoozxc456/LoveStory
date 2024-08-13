using LoveStory.IntegrationTest.Providers;
using LoveStory.WebApi.Controllers;
using LoveStory.WebApi.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace LoveStory.IntegrationTest.Controllers;

[TestFixture]
public class AuthControllerTest
{
    private IServiceProvider _provider;
    private AuthController _controller;
    private LoginRequestModel _authData;

    [SetUp]
    public void SetUp()
    {
        SetUpServiceProvider();
        SetUpController();
    }

    private void SetUpServiceProvider()
    {
        _provider = new ServiceCollectionProvider().Provider;
    }

    private void SetUpController()
    {
        _controller = new AuthController(_provider);
    }

    [Test]
    public void AuthOriginLogin_GivenCorrectAuthData_ShouldReturnOkObjectResult()
    {
        SetValidAuthData();
        var response = _controller.AuthOriginLogin(_authData).Result;
        Assert.That(response, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void AuthOriginLogin_GivenNotExistAuthData_ShouldBeThrowException()
    {
        SetNotExistAuthData();
        var response = _controller.AuthOriginLogin(_authData);
        Assert.CatchAsync<Exception>(() => response);
    }

    [Test]
    public void AuthOriginLogin_GivenInvalidPasswordAuthData_ShouldReturnOkObjectResult()
    {
        SetInvalidAuthData();
        var response = _controller.AuthOriginLogin(_authData).Result;
        Assert.That(response, Is.InstanceOf<OkObjectResult>());
    }

    private void SetValidAuthData() => _authData = new LoginRequestModel
    {
        Password = "test_exist_user_password",
        Username = "test_exist_user"
    };

    private void SetNotExistAuthData() => _authData = new LoginRequestModel
    {
        Password = "test_non_exist_user_password",
        Username = "test_non_exist_user"
    };

    private void SetInvalidAuthData() => _authData = new LoginRequestModel
    {
        Password = "test_invalid_password",
        Username = "test_exist_user"
    };
}