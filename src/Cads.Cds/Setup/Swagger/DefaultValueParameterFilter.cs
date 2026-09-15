using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Text.Json.Nodes;

namespace Cads.Cds.Setup.Swagger;

/// <summary>
/// Surfaces [DefaultValue] on query parameters whose schema is a reference, such as enums.
/// A $ref may not carry sibling keywords, so the reference is wrapped in allOf and the
/// default applied alongside it.
/// </summary>
public class DefaultValueParameterFilter : IParameterFilter
{
    public void Apply(IOpenApiParameter parameter, ParameterFilterContext context)
    {
        var defaultValue = context.ParameterInfo?.GetCustomAttributes(typeof(DefaultValueAttribute), inherit: true)
            .Cast<DefaultValueAttribute>()
            .FirstOrDefault()?.Value
            ?? context.PropertyInfo?.GetCustomAttributes(typeof(DefaultValueAttribute), inherit: true)
            .Cast<DefaultValueAttribute>()
            .FirstOrDefault()?.Value;

        if (defaultValue is null
            || parameter is not OpenApiParameter concrete
            || concrete.Schema is not OpenApiSchemaReference reference)
            return;

        concrete.Schema = new OpenApiSchema
        {
            AllOf = [reference],
            Default = JsonValue.Create(defaultValue.ToString())
        };
    }
}
