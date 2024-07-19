using LoveStory.Infrastructure.Configurations;
using LoveStory.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<InfrastructureConfiguration>();

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

app.Run();