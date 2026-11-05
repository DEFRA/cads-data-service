using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.SystemAdmin.Application.Generation.Scenarios;
using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Scenarios;

public abstract class GenerationScenario<T>(string name,
    ImportActionType importActionType,
    DbContext dbContext,
    IFileNameGenerator fileNameGenerator,
    IFileAssembler fileAssembler) : IGenerationScenario
    where T : class, new()
{
    private const string FileNameApplicationPrefix = CtsmFilenameGenerator.App;

    private const string FileNameEnvironmentPrefix = CtsmFilenameGenerator.Env;

    public string Name { get; init; } = name;

    public string TableName => new ContentGenerator<T>(dbContext).TableName;

    protected virtual RuleBuilder<T>? OverrideRulesBuilder { get; init; }

    protected virtual Func<T, decimal, T>? Transform { get; init; }

    public async Task<CreateGenerationResponseDto> ExecuteAsync(CreateGenerationRequestDto request, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        var contentGenerator = new ContentGenerator<T>(dbContext, OverrideRulesBuilder, Transform);
        var tableName = contentGenerator.TableName;

        /*
        TODO: Caller-supplied keys bypass allocation. 
        request.BusinessKeys ?? allocate lets a bulk request choose its own keys and skip the reserved range.
        A rowCount-only update request allocates fresh keys and “updates” rows that don’t exist.
        Validate this in the request validator: bulk takes no keys, and update requires keys.
        */
        var businessKeys = request.BusinessKeys ??
            await new BusinessKeysAllocator().AllocateAsync(tableName, request.RowCount.GetValueOrDefault(), ct);

        /*
        TODO: LANI-849 had this so it could be regenerated.
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

        /*
        TODO: Delta details are not visible here. fileAssembler.Create(fileName, date, rows) has no aud ID or aud type arguments,
        so check where the delta layout and U come from.The update scenario also needs the aud IDs allocated per row.
        */
        var content = fileAssembler.Create(fileName, fileCreatedDateTime, rows);

        return new CreateGenerationResponseDto
        {
            FileName = fileName,
            Content = content,
            BusinessKeys = businessKeys
        };
    }
}