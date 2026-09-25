using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Entities;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Rules;
using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Scenarios.CtLocationScenarios;

public class CtLocationBulkScenario(
    CtsTransactionsSystemAdminDbContext dbContext,
    IFileNameGenerator fileNameGenerator,
    IFileAssembler fileAssembler)
    : GenerationScenario<CtLocation>("ct_location_bulk_scenario", ImportActionType.Bulk, dbContext, fileNameGenerator, fileAssembler)
{
    protected override RuleBuilder<CtLocation>? OverrideRulesBuilder { get; init; } =
        new RuleBuilder<CtLocation>([])
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
            entity.TransType = "B";
            entity.LocId = key;
            return entity;
        };
}