namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public interface IPostgresIamTokenGenerator
{
    Task<string> GenerateAuthTokenAsync(string hostname, int port, string username);
}