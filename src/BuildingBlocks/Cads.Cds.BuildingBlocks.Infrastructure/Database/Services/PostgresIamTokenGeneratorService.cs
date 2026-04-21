using Amazon;
using Amazon.RDS.Util;
using Amazon.Runtime;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public class PostgresIamTokenGeneratorService(AWSCredentials credentials, RegionEndpoint region) : IPostgresIamTokenGeneratorService
{
    public Task<string> GenerateAuthTokenAsync(string hostname, int port, string username)
    {
        var token = RDSAuthTokenGenerator.GenerateAuthToken(credentials, region, hostname, port, username);
        return Task.FromResult(token);
    }
}