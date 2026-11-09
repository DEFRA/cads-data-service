using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;

public class BusinessKeysAllocator : IBusinessKeysAllocator
{
    /*
    TODO: The allocator is still the stub. new BusinessKeysAllocator() returns 1..N, which collides with seed rows.
    It needs the LANI-848 sequence, injected as IBusinessKeysAllocator instead of constructed inline.
    */
    public async Task<IReadOnlyList<decimal>> AllocateAsync(string table, int rowCount, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        var keys = new List<decimal>(rowCount);

        for (var i = 1; i <= rowCount; i++)
        {
            keys.Add(i);
        }

        return keys;
    }
}