using Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators.Rules;
using Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.CtsTransactions.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators.Schemas.CtsTransactions;

public sealed class CtLocationContentGenerator(DbContext dbContext) 
    : ContentGenerator<CtLocation>(dbContext), IContentGenerator
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
    protected override Func<CtLocation, decimal, CtLocation>? BulkTransform { get; init; } =
        (entity, key) =>
        {
            entity.TransType = "B";
            entity.LocId = key;
            return entity;
        };

    // configure the transform for updates
    protected override Func<CtLocation, decimal, CtLocation>? UpdateTransform { get; init; } =
        (entity, key) =>
        {
            entity.TransType = "U";
            entity.LocId = key;
            return entity;
        };
}