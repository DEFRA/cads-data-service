using System.ComponentModel.DataAnnotations;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;

public class AclOptions
{
    public Dictionary<string, ApiKeyClient> Clients { get; init; } = [];

    public class ApiKeyClient
    {
        [Required]
        public required string Secret { get; init; }

        [Required]
        public required string[] Scopes { get; init; } = [];
    }
}