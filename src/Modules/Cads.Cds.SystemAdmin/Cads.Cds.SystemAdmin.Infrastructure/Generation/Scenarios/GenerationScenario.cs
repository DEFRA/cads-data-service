using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;
using Cads.Cds.SystemAdmin.Application.Generation.Scenarios;
using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Scenarios;

public abstract class GenerationScenario<T>(string name,
    ImportActionType importActionType,
    DbContext dbContext,
    IFileNameGenerator fileNameGenerator,
    IFileAssembler fileAssembler,
    IContentGeneratorFactory contentGeneratorFactory,
    IEnvelopeTableResolver envelopeTableResolver) : IGenerationScenario
    where T : class, new()
{
    private const string FileNameApplicationPrefix = CtsmFilenameGenerator.App;

    private const string FileNameEnvironmentPrefix = CtsmFilenameGenerator.Env;

    public string Name { get; init; } = name;

    public string TableName => contentGeneratorFactory.Create<T>(dbContext).TableName;

    protected virtual RuleBuilder<T>? OverrideRulesBuilder { get; init; }

    protected virtual Func<T, decimal, T>? Transform { get; init; }

    /// <summary>Delta only: the aud type (I, U or D) written to every row. Bulk scenarios leave it null.</summary>
    protected virtual string? AudType { get; init; }

    public async Task<CreateGenerationResponseDto> ExecuteAsync(CreateGenerationRequestDto request, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        var contentGenerator = contentGeneratorFactory.Create(dbContext, OverrideRulesBuilder, Transform);
        var tableName = contentGenerator.TableName;

        /*
        TODO: Caller-supplied keys bypass allocation. 
        request.BusinessKeys ?? allocate lets a bulk request choose its own keys and skip the reserved range.
        A rowCount-only update request allocates fresh keys and “updates” rows that don’t exist.
        Validate this in the request validator: bulk takes no keys, and update requires keys.
        LANI-848 is done in the Allocator. This is to validate supplied values when it is a bulk or delta/update scenario only.
        */
        var businessKeys = request.BusinessKeys ??
            await new BusinessKeysAllocator().AllocateAsync(tableName, request.RowCount.GetValueOrDefault(), ct);

        /*
        TODO: LANI-849 had this so it could be regenerated. Need to fulfil the requirements from LANI-849.
        */
        var seed = Random.Shared.Next();
        var rows = contentGenerator.Generate(businessKeys, seed);

        var fileBatchId = Random.Shared
            .Next(CtsmFilenameGenerator.MinBatchId, CtsmFilenameGenerator.MaxBatchId + 1)
            .ToString("D5", CultureInfo.InvariantCulture);

        var fileCreatedDateTime = DateTime.UtcNow;
        var fileName = fileNameGenerator.Create(
            new CreateFileNameCommand
            {
                App = FileNameApplicationPrefix,
                Env = FileNameEnvironmentPrefix,
                Type = importActionType,
                BatchId = fileBatchId,
                TableName = tableName,
                Timestamp = fileCreatedDateTime
            });

        var parsedFileName = CtsmFilenameParser.Parse(fileName);

        var options = await CreateAssemblyOptionsAsync(tableName, rows.Count, ct);

        var table = envelopeTableResolver.Resolve(dbContext.Model, typeof(T));

        var content = fileAssembler.Create(fileName, fileCreatedDateTime, table, rows, options);

        return new CreateGenerationResponseDto
        {
            FileName = fileName,
            GroupKey = parsedFileName!.GetGroupKey(),
            Content = content,
            BusinessKeys = businessKeys
        };
    }

    private async Task<FileAssemblyOptions> CreateAssemblyOptionsAsync(string tableName, int rowCount, CancellationToken ct)
    {
        if (importActionType == ImportActionType.Bulk)
            return FileAssemblyOptions.Bulk();

        var audIds = await new BusinessKeysAllocator().AllocateAsync(tableName, rowCount, ct);

        return new FileAssemblyOptions(
            FileLayout.Delta,
            AudType,
            [.. audIds.Select(id => (long)id)]);
    }
}
