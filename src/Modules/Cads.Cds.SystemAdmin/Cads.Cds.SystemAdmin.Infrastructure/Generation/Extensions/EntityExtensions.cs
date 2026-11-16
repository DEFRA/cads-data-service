using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Extensions;

public static class EntityExtensions
{
    public static IReadOnlyDictionary<string, object?> ToDictionary<T>(this T entity)
        where T : class, new()
    {
        return typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead)
            .ToDictionary(p => p.Name, p => p.GetValue(entity));
    }
}
