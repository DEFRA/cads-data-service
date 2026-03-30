using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;

namespace Cads.Cds.Ingester.Tests.Component.TestFixtures;

public class IngesterWebApplicationFactory(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebAppFactoryBase<Program>(
    configOverrides: configOverrides,
    useFakeAuth: useFakeAuth)
{
}