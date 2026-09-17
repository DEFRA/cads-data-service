using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Text.Json.Nodes;

namespace Cads.Cds.Setup.Swagger;

public class DefaultValueParameterFilter : IParameterFilter
{
    public void Apply(IOpenApiParameter parameter, ParameterFilterContext context)
    {
        var defaultValue = GetExplicitDefaultValue(context) ?? GetImplicitDefaultValue(context);

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

    private static object? GetExplicitDefaultValue(ParameterFilterContext context)
    {
        return context.ParameterInfo?.GetCustomAttributes(typeof(DefaultValueAttribute), inherit: true)
            .Cast<DefaultValueAttribute>()
            .FirstOrDefault()?.Value
            ?? context.PropertyInfo?.GetCustomAttributes(typeof(DefaultValueAttribute), inherit: true)
            .Cast<DefaultValueAttribute>()
            .FirstOrDefault()?.Value;
    }

    private static object? GetImplicitDefaultValue(ParameterFilterContext context)
    {
        var property = context.PropertyInfo;
        if (property?.DeclaringType is null)
            return null;

        try
        {
            var instance = Activator.CreateInstance(property.DeclaringType);
            return property.GetValue(instance);
        }
        catch
        {
            return null;
        }
    }
}