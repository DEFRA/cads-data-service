namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public class PostgresStatusServiceResult
{
    public bool CanConnect { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}