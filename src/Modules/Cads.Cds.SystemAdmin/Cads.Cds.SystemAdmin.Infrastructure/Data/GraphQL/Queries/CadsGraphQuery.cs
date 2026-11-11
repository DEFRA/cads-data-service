using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Types;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cads.Contexts;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Queries;

public partial class CadsGraphQuery
{
    public async Task<FileImportBatch?> GetFileImportBatch(
        string groupKey,
        [Service] CadsGQDbContext db,
        CancellationToken cancellationToken)
    {
        var imports = await db.CtsFileImports
            .Where(x => x.GroupKey == groupKey)
            .ToListAsync(cancellationToken);

        if (imports.Count == 0)
        {
            return null;
        }

        var parts = imports
            .Select(x => new FileImportBatchPart
            {
                PartNo = CtsmFilenameParser.TryParse(x.FileName, out var parsed) && parsed!.PartNo is not null
                    ? int.Parse(parsed.PartNo)
                    : 0,
                FileName = x.FileName,
                ImportStatusId = x.ImportStatusId,
                ProcessingStatusId = x.ProcessingStatusId,
                TotalRowsToProcess = x.TotalRowsToProcess,
                RowsFound = x.RowsFound,
                RowsImported = x.RowsImported,
                FailedAttempts = x.FailedAttempts,
                LastErrorReason = x.LastErrorReason,
                LastFilePartImported = x.LastFilePartImported
            })
            .OrderBy(p => p.PartNo)
            .ToList();

        return new FileImportBatch
        {
            GroupKey = groupKey,
            ImportComplete = imports.All(x => x.ImportStatusId == (short)FileImportStatus.Completed)
                && imports.Sum(x => x.TotalRowsToProcess) == imports.Sum(x => x.RowsFound),
            ProcessingComplete = imports.All(x => x.ProcessingStatusId == (short)FileProcessingStatus.Complete),
            Parts = parts
        };
    }
}