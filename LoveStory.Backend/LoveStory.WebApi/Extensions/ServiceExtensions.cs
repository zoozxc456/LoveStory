using LoveStory.Core.Interfaces;
using LoveStory.Core.Mappers;
using LoveStory.Core.Securities;
using LoveStory.Core.Services;
using LoveStory.Infrastructure.Contexts;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using LoveStory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoveStory.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void InjectServices(this IServiceCollection collection)
    {
        collection.InjectRepositories();
        collection.InjectCoreServices();
        collection.InjectProviders();
        collection.InjectMappers();
    }

    public static void InjectDbContexts(this IServiceCollection collection, List<string> connectionStrings)
    {
        collection.AddDbContext<LoveStoryContext>(option =>
        {
            connectionStrings.ForEach(connectionString => option.UseSqlServer(connectionString));
        });
    }

    private static void InjectMappers(this IServiceCollection collection)
    {
        collection.AddAutoMapper(typeof(Mapper));
    }

    private static void InjectRepositories(this IServiceCollection collection)
    {
        collection.AddScoped<IRepository<UserData>, GenericRepository<UserData>>();
        collection.AddScoped<IRepository<BanquetTableData>, GenericRepository<BanquetTableData>>();
        collection.AddScoped<IRepository<GuestData>, GuestRepository>();
        collection.AddScoped<IRepository<GuestAttendanceData>, GenericRepository<GuestAttendanceData>>();
        collection.AddScoped<IGuestGroupRepository, GuestGroupRepository>();
        collection.AddScoped<IRepository<GuestSpecialNeedData>, GenericRepository<GuestSpecialNeedData>>();
    }

    private static void InjectCoreServices(this IServiceCollection collection)
    {
        collection.AddScoped<IGuestService, GuestService>();
        collection.AddScoped<IGuestManagementService, GuestService>();
        collection.AddScoped<ILoginService, LoginService>();
        collection.AddScoped<IBanquetTableService, BanquetTableService>();
        collection.AddScoped<IUserService, UserService>();
    }

    private static void InjectProviders(this IServiceCollection collection)
    {
        collection.AddSingleton<IHashProvider, Argon2HashProvider>();
        collection.AddSingleton<IAccessTokenProvider, JwtAccessTokenProvider>();
    }
}