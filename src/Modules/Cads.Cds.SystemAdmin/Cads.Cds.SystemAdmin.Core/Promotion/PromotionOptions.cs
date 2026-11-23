namespace Cads.Cds.SystemAdmin.Core.Promotion;

public class PromotionOptions
{
    public required Dictionary<string, bool> PromotionTypes { get; init; }

    public IReadOnlyDictionary<string, bool> GetPromotionTypes()
    {
        return PromotionTypes.ToDictionary(
            x => x.Key,
            x => x.Value);
    }

    public bool IsEnabled(PromotionType promotionType)
    {
        return PromotionTypes.TryGetValue(
            promotionType.ToString(),
            out var enabled)
            && enabled;
    }
}