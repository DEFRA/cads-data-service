using System.Reflection;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Methods;

public static class MethodInfoUtility
{
    public static MethodInfo GetPrivate<T>(string name)
    {
        return typeof(T)
            .GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new InvalidOperationException($"Method {name} not found");
    }

    public static MethodInfo GetPrivateStatic<T>(string name)
    {
        return typeof(T)
            .GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static)
            ?? throw new InvalidOperationException($"Method {name} not found");
    }
}