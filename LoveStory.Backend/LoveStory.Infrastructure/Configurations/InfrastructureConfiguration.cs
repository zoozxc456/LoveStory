using Microsoft.Extensions.Configuration;

namespace LoveStory.Infrastructure.Configurations;

public class InfrastructureConfiguration(IConfiguration configuration)
{
    public string ConnectionString { get; } = configuration.GetConnectionString("LoveStorySqlServer") ?? string.Empty;
}