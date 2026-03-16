using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;

namespace Cads.Cds.MiBff.Tests.Component.TestFixtures;

public class MiBffWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
        configOverrides: configOverrides,
        useFakeAuth: useFakeAuth)
{
}