using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using IFS.Person.WebApi.Infrastructure.Attributes;

namespace IFS.Person.WebApi.Infrastructure.Filters;

public class ApiKeyAuthFilter : IAsyncActionFilter
{
    private const string ApiKeyHeader = "x-api-key";
    private readonly AppSettings _appSettings;

    public ApiKeyAuthFilter(IOptions<AppSettings> options)
    {
        _appSettings = options.Value;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (_appSettings.IsImpersonated)
        {
            await next();
            return;
        }

        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var apiKeyHeaderValue))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var domainApiKeyAuthAttribute = context.ActionDescriptor.FilterDescriptors.Select(p => p.Filter).OfType<DomainApiKeyAuthAttribute>().FirstOrDefault();
        if (domainApiKeyAuthAttribute != null)
        {
            var domainApiKey = _appSettings.DomainApiKey;
            if (!domainApiKey.Equals(apiKeyHeaderValue))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
            return;
        }

        context.Result = new UnauthorizedResult();
    }
}