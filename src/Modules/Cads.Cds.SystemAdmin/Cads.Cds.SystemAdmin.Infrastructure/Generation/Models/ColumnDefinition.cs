namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Models;

public sealed class ColumnDefinition(string propertyName, bool isVisible = true, int? order = null, string? caption = null)
{
    public string PropertyName { get; set; } = propertyName;
    public bool IsVisible { get; set; } = isVisible;
    public int? Order { get; set; } = order;
    public string? Caption { get; set; } = caption;
}