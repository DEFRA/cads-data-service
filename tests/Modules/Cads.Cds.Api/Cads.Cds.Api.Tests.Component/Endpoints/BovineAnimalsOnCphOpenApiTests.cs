using Cads.Cds.Api.Testing.Support.Constants;
using Cads.Cds.Api.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace Cads.Cds.Api.Tests.Component.Endpoints;

public class BovineAnimalsOnCphOpenApiTests(ApiTestFixture testFixture) : IClassFixture<ApiTestFixture>
{
    private const string SwaggerDocument = "/swagger/v1/swagger.json";

    [Fact]
    public async Task WhenTheOpenApiDocumentIsRequested_ShouldBeServed()
    {
        var response = await testFixture.HttpClient.GetAsync(SwaggerDocument, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task WhenTheOpenApiDocumentIsRequested_ShouldPublishTheAnimalsOnCphEndpoint()
    {
        var operation = await GetOperation();

        operation.GetProperty("responses").EnumerateObject().Select(r => r.Name)
            .Should().Contain(["200", "400", "404"]);
    }

    [Theory]
    [InlineData("CPH")]
    [InlineData("holdingAssociation")]
    [InlineData("status")]
    [InlineData("sex")]
    [InlineData("breedCode")]
    [InlineData("dateOnCPHFrom")]
    [InlineData("q")]
    [InlineData("page")]
    [InlineData("pageSize")]
    [InlineData("orderBy")]
    [InlineData("direction")]
    public async Task WhenTheOpenApiDocumentIsRequested_ShouldPublishTheSupportedQueryParameter(string name)
    {
        var parameters = await GetParameters();

        parameters.Should().ContainKey(name);
    }

    [Theory]
    [InlineData("holdingAssociation", "MovedOnHolding")]
    [InlineData("orderBy", "Identifier")]
    [InlineData("direction", "Asc")]
    public async Task WhenTheOpenApiDocumentIsRequested_ShouldDeclareTheParameterDefault(string name, string expected)
    {
        var parameters = await GetParameters();

        var schema = parameters[name].GetProperty("schema");

        schema.GetProperty("default").ToString().Should().Be(expected);
    }

    [Theory]
    [InlineData("HoldingAssociation", new[] { "MovedOnHolding", "RegisteredOnHolding" })]
    [InlineData("AnimalStatus", new[] { "Alive", "Dead", "OffFarm", "Unknown" })]
    [InlineData("AnimalSex", new[] { "Female", "Male" })]
    [InlineData("AnimalsOnHoldingOrderBy", new[] { "Identifier", "BirthDate", "DateOnCPH", "Sex", "BreedCode" })]
    [InlineData("SortDirection", new[] { "Asc", "Desc" })]
    public async Task WhenTheOpenApiDocumentIsRequested_ShouldDeclareTheValueSet(string schema, string[] expected)
    {
        var document = await GetDocument();

        var values = document.RootElement
            .GetProperty("components")
            .GetProperty("schemas")
            .GetProperty(schema)
            .GetProperty("enum")
            .EnumerateArray()
            .Select(v => v.GetString());

        values.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("identifier")]
    [InlineData("birthDate")]
    [InlineData("dateOnCPH")]
    [InlineData("dateOffCPH")]
    [InlineData("species")]
    [InlineData("sex")]
    [InlineData("breedCode")]
    [InlineData("status")]
    public async Task WhenTheOpenApiDocumentIsRequested_ShouldDeclareTheAnimalField(string field)
    {
        var document = await GetDocument();

        document.RootElement
            .GetProperty("components")
            .GetProperty("schemas")
            .GetProperty("AnimalSummaryDto")
            .GetProperty("properties")
            .TryGetProperty(field, out _)
            .Should().BeTrue();
    }

    [Theory]
    [InlineData("resourceType")]
    [InlineData("animals")]
    public async Task WhenTheOpenApiDocumentIsRequested_ShouldDeclareTheCollectionField(string field)
    {
        var document = await GetDocument();

        document.RootElement
            .GetProperty("components")
            .GetProperty("schemas")
            .GetProperty("AnimalsOnHoldingDto")
            .GetProperty("properties")
            .TryGetProperty(field, out _)
            .Should().BeTrue();
    }

    private async Task<Dictionary<string, JsonElement>> GetParameters()
    {
        var operation = await GetOperation();

        return operation.GetProperty("parameters")
            .EnumerateArray()
            .ToDictionary(p => p.GetProperty("name").GetString()!, p => p);
    }

    private async Task<JsonElement> GetOperation()
    {
        var document = await GetDocument();

        return document.RootElement
            .GetProperty("paths")
            .GetProperty($"/{TestEndpointConstants.ApiBovineAnimals.TrimStart('/')}")
            .GetProperty("get");
    }

    private async Task<JsonDocument> GetDocument()
    {
        var response = await testFixture.HttpClient.GetAsync(SwaggerDocument, TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();

        return JsonDocument.Parse(await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken));
    }
}