namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;

public interface IPostgresIamTokenGeneratorService
{
    Task<string> GenerateAuthTokenAsync(string hostname, int port, string username);
}