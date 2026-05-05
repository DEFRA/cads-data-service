using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Cads.Cds.Middleware;

public class ProducesResponseTypeOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attrs = context.MethodInfo.GetCustomAttributes<ProducesResponseTypeAttribute>();

        foreach (var attr in attrs)
        {
            operation.Responses[attr.StatusCode.ToString()] = new OpenApiResponse
            {
                Description = attr.Description ?? $"Response {attr.StatusCode}"
            };
        }
    }
}