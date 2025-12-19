using Microsoft.AspNetCore.Mvc.Filters;

namespace IFS.Person.WebApi.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DomainApiKeyAuthAttribute : Attribute, IFilterMetadata
{
}