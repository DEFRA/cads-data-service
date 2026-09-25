namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;

public interface IPostgresPoolRegistry
{
    IReadOnlyCollection<string> Identifiers { get; }
    bool IsKnown(string identifier);
    bool IsReadPool(string identifier);
}
