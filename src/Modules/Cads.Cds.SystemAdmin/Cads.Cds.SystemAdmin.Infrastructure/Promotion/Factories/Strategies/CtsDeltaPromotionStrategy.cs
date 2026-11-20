using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.SystemAdmin.Core.Promotion;

namespace Cads.Cds.SystemAdmin.Infrastructure.Promotion.Factories.Strategies;

public class CtsDeltaPromotionStrategy(IPostgresDataSourceFactory factory) : PostgresPromotionStrategy(factory)
{
    public override PromotionType PromotionType => PromotionType.CtsDelta;
    protected override string ProcedureName => "prepare_cts_parallel_delta_import";
}