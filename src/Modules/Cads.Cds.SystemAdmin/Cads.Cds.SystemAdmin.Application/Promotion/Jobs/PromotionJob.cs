using Cads.Cds.SystemAdmin.Core.Promotion;

namespace Cads.Cds.SystemAdmin.Application.Promotion.Jobs;

public sealed record PromotionJob(PromotionType PromotionType, string CorrelationId, bool? DeleteSource, string? RequestedBy);