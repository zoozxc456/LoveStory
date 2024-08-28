using System.Net.Http.Headers;
using LoveStory.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace LoveStory.WebApi.Controllers;

public class BaseController(IServiceProvider provider) : Controller
{
    protected Guid UserId;
    private readonly IAccessTokenProvider _accessTokenProvider = provider.GetRequiredService<IAccessTokenProvider>();

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        var token = GetAuthorizeToken(context);

        var principal = ValidAccessToken(token);
        Guid.TryParse(GetSubjectAttribute(principal, token) as string, out var result);

        UserId = result;
    }

    private static object? GetSubjectAttribute(TokenValidationResult principal, string token)
    {
        if (principal.Claims.TryGetValue(JwtRegisteredClaimNames.Sub, out var subject))
        {
            return subject;
        }

        throw new Exception($"Token: {token} is valid, but not found Subject attribute");
    }

    private TokenValidationResult ValidAccessToken(string token)
    {
        var principal = _accessTokenProvider.ValidateAccessToken(token);

        if (principal.IsValid)
        {
            return principal;
        }

        throw new Exception($"Token: {token} is invalid");
    }

    private static string GetAuthorizeToken(ActionContext context)
    {
        var authorization = context.HttpContext.Request.Headers.Authorization;
        if (AuthenticationHeaderValue.TryParse(authorization, out var token) && token.Parameter != null)
        {
            return token.Parameter;
        }

        throw new Exception("Authorize Token Error");
    }
}