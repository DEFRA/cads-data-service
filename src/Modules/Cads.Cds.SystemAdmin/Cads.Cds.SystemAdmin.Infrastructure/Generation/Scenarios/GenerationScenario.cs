using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.SystemAdmin.Application.Generation.Scenarios;
using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Scenarios;

public abstract class GenerationScenario<T>(string name,
    ImportActionType importActionType, DbContext dbContext,
    IFileNameGenerator fileNameGenerator,
    IFileAssembler fileAssembler) : IGenerationScenario
    where T : class, new()
{
    private const string FileNameApplicationPrefix = "UKV";

    private const string FileNameEnvironmentPrefix = "DEV";

    private const string FileNameBatchId = "BATCH001";

    public string Name { get; init; } = name;

    protected virtual RuleBuilder<T>? OverrideRulesBuilder { get; init; }

    protected virtual Func<T, decimal, T>? Transform { get; init; }

    private readonly ImportActionType _importActionType = importActionType;

    public string TableName => new ContentGenerator<T>(dbContext).TableName;

    public async Task<CreateGenerationResponseDto> ExecuteAsync(CreateGenerationRequestDto request, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        var contentGenerator = new ContentGenerator<T>(dbContext, OverrideRulesBuilder, Transform);
        var tableName = contentGenerator.TableName;
        var businessKeysAllocator = new BusinessKeysAllocator();
        var businessKeys = await businessKeysAllocator.AllocateAsync(tableName, request.RowCount, ct);
        var seed = Random.Shared.Next();
        var rows = contentGenerator.Generate(businessKeys, seed);

        var fileCreatedDateTime = DateTime.UtcNow;
        var fileName = fileNameGenerator.Create(FileNameApplicationPrefix, FileNameEnvironmentPrefix, _importActionType, FileNameBatchId, tableName, fileCreatedDateTime);
        var content = fileAssembler.Create(fileName, fileCreatedDateTime, rows);

        return new CreateGenerationResponseDto
        {
            FileName = fileName,
            Content = System.Text.Json.JsonSerializer.Serialize(content),
            BusinessKeys = businessKeys
        };
    }
}