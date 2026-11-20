using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsTransactions.Entities;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Models;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Scenarios.CtLocationScenarios;

public class CtLocationBulkScenario(
    CtsTransactionsGQDbContext dbContext,
    IFileNameGenerator fileNameGenerator,
    IFileAssembler fileAssembler)
    : GenerationScenario<CtLocation>("ct_location_bulk_scenario", ImportActionType.Bulk, dbContext, fileNameGenerator, fileAssembler)
{
    protected override RuleBuilder<CtLocation>? OverrideRulesBuilder { get; init; } =
        new RuleBuilder<CtLocation>([])
            .RuleFor(r => r.RecordType, () => "D")
            .RuleFor(r => r.RecordCount, (f) => f.IndexFaker + 1)
            .RuleFor(r => r.LocSltId, () => 1m)
            .RuleFor(r => r.LocLtyId, () => 1m)
            .RuleFor(r => r.LocCtyId, () => 1m)
            .RuleFor(r => r.LocReceivePpafFlag, () => "Y")
            .RuleFor(r => r.LocReceiveLabelsFlag, () => "N")
            .RuleFor(r => r.LocEffectiveFrom, () => DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30)))
            .RuleFor(r => r.LocEffectiveTo, () => null)
            .RuleFor(r => r.LocCessationReason, () => null)
            .RuleFor(r => r.LocPremisesType, () => "FARM")
            .RuleFor(r => r.LocReasonCode, () => "01");

    // configure the per-item transform for bulk generation
    protected override Func<CtLocation, decimal, CtLocation>? Transform { get; init; } =
        (entity, key) =>
        {
            entity.LocId = key;
            return entity;
        };

    protected override IReadOnlyDictionary<string, ColumnDefinition> OverrideColumnDefinitions(IReadOnlyDictionary<string, ColumnDefinition> defaultColumnDefinitions)
    {
        // Set order for RecordType and RecordCount to ensure they are the first two columns in the generated output
        defaultColumnDefinitions[nameof(CtLocation.RecordType)].Order = 0;
        defaultColumnDefinitions[nameof(CtLocation.RecordCount)].Order = 1;

        // Exclude columns that should not be visible in the generated output for the bulk scenario
        defaultColumnDefinitions[nameof(CtLocation.TransId)].IsVisible = false;
        defaultColumnDefinitions[nameof(CtLocation.TransType)].IsVisible = false;
        defaultColumnDefinitions[nameof(CtLocation.RowNumber)].IsVisible = false;
        defaultColumnDefinitions[nameof(CtLocation.ImportedDate)].IsVisible = false;
        defaultColumnDefinitions[nameof(CtLocation.CtsFileImportId)].IsVisible = false;
        defaultColumnDefinitions[nameof(CtLocation.LocAudDatetime)].IsVisible = false;
        defaultColumnDefinitions[nameof(CtLocation.LocAudId)].IsVisible = false;
        defaultColumnDefinitions[nameof(CtLocation.LocAudType)].IsVisible = false;
        defaultColumnDefinitions[nameof(CtLocation.FakeData)].IsVisible = false;

        return defaultColumnDefinitions;
    }
}