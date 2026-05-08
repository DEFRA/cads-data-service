using Cads.Cds.BuildingBlocks.Application.OpenXml;
using Cads.Cds.BuildingBlocks.Core.OpenXml;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cads.Cds.BuildingBlocks.Application.Tests.Unit.OpenXml;

public class ReportDefinitionRegistryTests
{
    private readonly IServiceProvider _provider;
    private readonly Assembly _validAssembly;

    public ReportDefinitionRegistryTests()
    {
        _validAssembly = typeof(TestDefinition1).Assembly;

        var services = new ServiceCollection();

        services.AddTransient<IReportDefinition<TestModel1>, TestDefinition1>();
        services.AddTransient<IReportDefinition<TestModel2>, TestDefinition2>();

        _provider = services.BuildServiceProvider();
    }

    [Fact]
    public void Constructor_ShouldRegisterDefinitionsFromAssembly()
    {
        var registry = new ReportDefinitionRegistry(_provider, [_validAssembly]);

        registry.GetDefinition<TestModel1>().Should().BeOfType<TestDefinition1>();
        registry.GetDefinition<TestModel2>().Should().BeOfType<TestDefinition2>();
    }

    [Fact]
    public void GetDefinition_ShouldResolveDefinitionFromServiceProvider()
    {
        var registry = new ReportDefinitionRegistry(_provider, [_validAssembly]);

        var definition = registry.GetDefinition<TestModel1>();

        definition.Should().BeOfType<TestDefinition1>();
    }

    [Fact]
    public void GetDefinition_ShouldThrow_WhenDefinitionNotRegistered()
    {
        var registry = new ReportDefinitionRegistry(_provider, [_validAssembly]);

        Action act = () => registry.GetDefinition<string>();

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("No report definition registered for type String");
    }

    public class TestModel1 { }
    public class TestModel2 { }

    public class TestDefinition1 : IReportDefinition<TestModel1>
    {
        public string TemplateFileName => "file1.xlsx";
        public int TableTemplateRow => 5;
        public int TemplateRowFirstColumn => 1;
        public List<Func<TestModel1, IConvertible>> Selectors => [];
    }

    public class TestDefinition2 : IReportDefinition<TestModel2>
    {
        public string TemplateFileName => "file2.xlsx";
        public int TableTemplateRow => 6;
        public int TemplateRowFirstColumn => 2;
        public List<Func<TestModel2, IConvertible>> Selectors => [];
    }
}