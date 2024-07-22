using LoveStory.Core.Interfaces;
using LoveStory.Core.Services;
using LoveStory.Infrastructure.Contexts;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using LoveStory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IRepository<UserData>, GenericRepository<UserData>>();
builder.Services.AddScoped<IRepository<BanquetTableData>, GenericRepository<BanquetTableData>>();
builder.Services.AddScoped<IRepository<GuestData>, GenericRepository<GuestData>>();
builder.Services.AddScoped<IRepository<GuestAttendanceData>, GenericRepository<GuestAttendanceData>>();
builder.Services.AddScoped<IRepository<GuestGroupData>, GenericRepository<GuestGroupData>>();
builder.Services.AddScoped<IRepository<GuestSpecialNeedData>, GenericRepository<GuestSpecialNeedData>>();

builder.Services.AddDbContext<LoveStoryContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("LoveStorySqlServer");
    option.UseSqlServer(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
