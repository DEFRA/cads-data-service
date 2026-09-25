using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;

public class BusinessKeysAllocator : IBusinessKeysAllocator
{
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