namespace LoveStory.WebApi.Extensions;

public static class EnvironmentExtensions
{
    public static bool IsUat(this IHostEnvironment environment) => environment.IsEnvironment("UAT");
}