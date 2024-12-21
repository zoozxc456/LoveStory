using LoveStory.IntegrationTest.Providers;
using LoveStory.WebApi.Controllers;
using LoveStory.WebApi.RequestModel.Recipient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.IntegrationTest.Controllers;

[TestFixture]
public class RecipientControllerTest : IDisposable
{
    private ServiceCollection _serviceCollection;
    private IServiceProvider _provider;
    private RecipientController _recipientController;

    [SetUp]
    public void SetUp()
    {
        SetUpServiceProvider();
        SetUpRecipientController();
    }

    private void SetUpServiceProvider()
    {
        _provider = new ServiceCollectionProvider().Provider;
    }

    private void SetUpRecipientController()
    {
        _recipientController = new RecipientController(_provider);
    }

    [Test]
    public void GuestArrive_GivenCorrectGuestId_ShouldReturnOkResult()
    {
        var requestModel = new GuestArriveRequestModel
        {
            GuestId = Guid.Parse("f322dfe3-b3bf-45cf-8f57-197ad1f2df12")
        };

        var result = _recipientController.GuestArrive(requestModel);

        Assert.That(result, Is.InstanceOf<OkResult>());
    }

    public void Dispose()
    {
        _recipientController.Dispose();
    }
}