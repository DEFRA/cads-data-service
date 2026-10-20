using Cads.Cds.BuildingBlocks.Testing.Support.Constants;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers.Configuration;

public class TestAwsStsConfiguration
{
    public string Audience { get; } = TestAuthConstants.StsAudience;
    public string TrustedOrgId { get; } = TestAuthConstants.StsTrustedOrgId;
    public string Environment { get; } = TestAuthConstants.StsEnvironment;
    public bool RequireHttpsMetadata { get; } = false;
}
