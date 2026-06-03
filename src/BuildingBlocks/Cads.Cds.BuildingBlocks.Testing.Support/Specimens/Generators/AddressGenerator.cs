namespace Cads.Cds.BuildingBlocks.Testing.Support.Specimens.Generators;

public static class AddressGenerator
{
    private static readonly Random s_random = new();

    public static string? GenerateMapReference(bool allowNulls = true)
    {
        return allowNulls && s_random.Next(2) == 0 ? null : $"NN {s_random.Next(100, 999)} {s_random.Next(100, 999)}";
    }
}