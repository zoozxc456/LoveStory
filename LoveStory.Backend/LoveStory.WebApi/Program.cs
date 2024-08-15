using System.Text;
using LoveStory.Core.Interfaces;
using LoveStory.Core.Securities;
using LoveStory.Core.Services;
using LoveStory.Infrastructure.Contexts;
using LoveStory.Infrastructure.Data;
using LoveStory.Infrastructure.Interfaces;
using LoveStory.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mapper = LoveStory.Core.Mappers.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
// 註冊 Swagger 產生器
builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization Tests"
        });

        // catch Api Token
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    }
);
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IGuestManagementService, GuestService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IBanquetTableService, BanquetTableService>();
builder.Services.AddScoped<IRepository<UserData>, GenericRepository<UserData>>();
builder.Services.AddScoped<IRepository<BanquetTableData>, GenericRepository<BanquetTableData>>();
builder.Services.AddScoped<IRepository<GuestData>, GuestRepository>();
builder.Services.AddScoped<IRepository<GuestAttendanceData>, GenericRepository<GuestAttendanceData>>();
builder.Services.AddScoped<IGuestGroupRepository, GuestGroupRepository>();
builder.Services.AddScoped<IRepository<GuestSpecialNeedData>, GenericRepository<GuestSpecialNeedData>>();
builder.Services.AddSingleton<IHashProvider, Argon2HashProvider>();
builder.Services.AddSingleton<IAccessTokenProvider, JwtAccessTokenProvider>();

builder.Services.AddAutoMapper(typeof(Mapper));

builder.Services.AddDbContext<LoveStoryContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("LoveStorySqlServer");
    option.UseSqlServer(connectionString);
});

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