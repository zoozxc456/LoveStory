using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LoveStory.WebApi.Extensions;

public static class SwaggerExtensions
{
    public static void InjectSwagger(this IServiceCollection collection)
    {
        collection.AddSwaggerGen(options =>
        {
            options.SetSwaggerSecurityDefinition();
            options.SetSwaggerSecurityRequirement();
        });
    }

    private static void SetSwaggerSecurityDefinition(this SwaggerGenOptions options)
    {
        const string securityName = "Authorization";
        const string bearerFormat = "JWT";
        const string description = "JWT Authorization";

        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            Name = securityName,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = bearerFormat,
            In = ParameterLocation.Header,
            Description = description
        });
    }

    private static void SetSwaggerSecurityRequirement(this SwaggerGenOptions options)
    {
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    }
}