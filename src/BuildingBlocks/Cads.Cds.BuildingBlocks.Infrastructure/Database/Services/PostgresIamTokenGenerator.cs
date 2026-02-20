using Amazon;
using Amazon.RDS.Util;
using Amazon.Runtime;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public class PostgresIamTokenGenerator : IPostgresIamTokenGenerator
{
    private readonly AWSCredentials _credentials;
    private readonly RegionEndpoint _region;

    public PostgresIamTokenGenerator(AWSCredentials credentials, RegionEndpoint region)
    {
        _credentials = credentials;
        _region = region;
    }

    public Task<string> GenerateAuthTokenAsync(string hostname, int port, string username)
    {
        var token = RDSAuthTokenGenerator.GenerateAuthToken(_credentials, _region, hostname, port, username);
        return Task.FromResult(token);
    }
}