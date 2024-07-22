using LoveStory.Core.Interfaces;
using LoveStory.Core.Services;
using LoveStory.Infrastructure.Contexts;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using LoveStory.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        SetUpServiceCollection();
        SetUpServiceProvider();
        SetUpGuestController();       
    }
    
    private void SetUpGuestController()
    {
        _guestController = new GuestController(_provider);
    }

    private void SetUpServiceProvider()
    {
        _provider = _serviceCollection.BuildServiceProvider();
    }

    private  void SetUpServiceCollection()
    {
        _serviceCollection = [];
        _serviceCollection.AddControllers();
        _serviceCollection.AddDbContext<LoveStoryContext>(option=>option.UseInMemoryDatabase("love_story"));
        _serviceCollection.AddScoped<IGuestService, GuestService>();
        _serviceCollection.AddScoped<IRepository<UserData>, GenericRepository<UserData>>();
        _serviceCollection.AddScoped<IRepository<BanquetTableData>, GenericRepository<BanquetTableData>>();
        _serviceCollection.AddScoped<IRepository<GuestData>, GenericRepository<GuestData>>();
        _serviceCollection.AddScoped<IRepository<GuestAttendanceData>, GenericRepository<GuestAttendanceData>>();
        _serviceCollection.AddScoped<IRepository<GuestGroupData>, GenericRepository<GuestGroupData>>();
        _serviceCollection.AddScoped<IRepository<GuestSpecialNeedData>, GenericRepository<GuestSpecialNeedData>>();
    }

    [Test]
    public void GetAllGuests_GivenEmptyParameter_ShouldReturnOkResult()
    {
        var actionResult = _guestController.GetAllGuests();
        Assert.That(actionResult, Is.InstanceOf<OkResult>());
    }

    public void Dispose()
    {
        _guestController.Dispose();
    }
}