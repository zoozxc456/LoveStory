using System.Text;
using LoveStory.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File($"logs/log-{DateTime.Now:yyyy-MM-dd}.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.InjectServices();
builder.Services.InjectDbContexts([builder.Configuration.GetConnectionString("LoveStorySqlServer") ?? string.Empty]);
builder.Services.InjectSwagger();

builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", policy =>
    {
        policy.SetIsOriginAllowed(origin => true);
        policy.AllowCredentials();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    var secretKey = builder.Configuration.GetSection("JwtConfig:SecretKey").Value ?? string.Empty;
    opt.IncludeErrorDetails = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration.GetSection("JwtConfig:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("JwtConfig:WebAudience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("policy");
// app.UseHttpsRedirection();
app.MapControllers();

app.Run();