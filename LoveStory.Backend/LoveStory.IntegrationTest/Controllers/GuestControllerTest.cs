using LoveStory.IntegrationTest.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using GuestController = LoveStory.WebApi.Controllers.GuestController;

namespace LoveStory.IntegrationTest.Controllers;

[TestFixture]
public class GuestControllerTest : IDisposable
{
    private ServiceCollection _serviceCollection;
    private IServiceProvider _provider;
    private GuestController _guestController;

    [SetUp]
    public void SetUp()
    {
        SetUpServiceProvider();
        SetUpGuestController();
    }

    private void SetUpServiceProvider()
    {
        _provider = new ServiceCollectionProvider().Provider;
    }

    private void SetUpGuestController()
    {
        _guestController = new GuestController(_provider);
    }

    [Test]
    public void GetAllGuests_GivenEmptyParameter_ShouldReturnOkObjectResult()
    {
        var actionResult = _guestController.GetAllGuests();
        Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
    }

    public void Dispose()
    {
        _guestController.Dispose();
    }
}