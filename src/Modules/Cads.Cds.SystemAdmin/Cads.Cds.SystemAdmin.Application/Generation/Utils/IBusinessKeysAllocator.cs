namespace Cads.Cds.SystemAdmin.Application.Generation.Utils;

public interface IBusinessKeysAllocator
{
    Task<IReadOnlyList<decimal>> AllocateAsync(string table, int rowCount, CancellationToken ct = default);
}