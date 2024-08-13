using AutoMapper;
using LoveStory.Core.Interfaces;
using LoveStory.Core.Securities;
using LoveStory.Core.Services;
using LoveStory.Infrastructure.Contexts;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using LoveStory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LoveStory.IntegrationTest.Providers;

public class ServiceCollectionProvider
{
    private readonly IServiceCollection _serviceCollection;
    public IServiceProvider Provider { get; }

    public ServiceCollectionProvider()
    {
        _serviceCollection = new ServiceCollection();
        SetServices();
        Provider = _serviceCollection.BuildServiceProvider();
        SeedData();
    }

    private void SetServices()
    {
        _serviceCollection.AddControllers();
        _serviceCollection.AddDbContext<LoveStoryContext>(option => option.UseInMemoryDatabase("love_story"));
        _serviceCollection.AddScoped<IGuestService, GuestService>();
        _serviceCollection.AddScoped<IGuestManagementService, GuestService>();
        _serviceCollection.AddScoped<ILoginService, LoginService>();
        _serviceCollection.AddScoped<IBanquetTableService, BanquetTableService>();
        _serviceCollection.AddScoped<IRepository<UserData>, GenericRepository<UserData>>();
        _serviceCollection.AddScoped<IRepository<BanquetTableData>, GenericRepository<BanquetTableData>>();
        _serviceCollection.AddScoped<IRepository<GuestData>, GuestRepository>();
        _serviceCollection.AddScoped<IRepository<GuestAttendanceData>, GenericRepository<GuestAttendanceData>>();
        _serviceCollection.AddScoped<IGuestGroupRepository, GuestGroupRepository>();
        _serviceCollection.AddScoped<IRepository<GuestSpecialNeedData>, GenericRepository<GuestSpecialNeedData>>();
        _serviceCollection.AddSingleton<IHashProvider, Argon2HashProvider>();
        _serviceCollection.AddSingleton<IAccessTokenProvider, JwtAccessTokenProvider>();
        _serviceCollection.AddAutoMapper(typeof(Mapper));
    }

    private void SeedData()
    {
        var loveStoryContext = Provider.GetRequiredService<LoveStoryContext>();
        loveStoryContext.Users.AddRange([
            new UserData
            {
                Username = "test_exist_user",
                Salted = "HelloWorld123456",
                Password = "d50aa5030f7e5187f80425491e8ff4004af06bdac5b896dd38aaf21c2df6852a"
            }
        ]);

        loveStoryContext.SaveChanges();
    }
}