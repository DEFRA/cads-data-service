using Cads.Cds.BuildingBlocks.Application.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Imports.Setup;
using Cads.Cds.SystemAdmin.Application.Generation.Scenarios;
using Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.CtsTransactions.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Tests.Unit.Generation;

public class GenerationServiceRegistrationTests
{
    [Fact]
    public void Scenarios_ResolveFromTheContainer()
    {
        var services = new ServiceCollection()
            .AddBuildBlocksApplicationLayer()
            .AddImportEnvelope()
            .ConfigureSystemAdminGeneration()
            .AddDbContext<CtsTransactionsGQDbContext>(o => o.UseInMemoryDatabase(nameof(Scenarios_ResolveFromTheContainer)));

        using var provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true });
        using var scope = provider.CreateScope();

        var scenarios = scope.ServiceProvider.GetServices<IGenerationScenario>().ToList();

        Assert.Equal(["ct_location_bulk_scenario", "ct_location_update_scenario"], scenarios.Select(s => s.Name));
    }
}
