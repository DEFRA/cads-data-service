using AutoFixture;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption.Providers;
using Cads.Cds.BuildingBlocks.Testing.Support.Payloads;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Encryption;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Cads.Cds.Ingester.Core.DTOs.Common;
using Cads.Cds.Ingester.Testing.Support.Constants;
using FluentAssertions;
using Newtonsoft.Json;
using System.Text;

namespace Cads.Cds.Ingester.Tests.Integration.AnimalMovementsEndpoints;

[Collection("IngesterIntegration"), Trait("Dependence", "testcontainers")]
public class AnimalMovementsEnpointTests(ApiContainerFixture apiContainerFixture)
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task GivenValidAnimalMovements_WhenPostingToEndpointV1_ShouldSucceed()
    {
        var nation = Nation.Wales;
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV1, nation);
        var client = apiContainerFixture.CreateBasicClient();

        var model = _fixture.Build<Controllers.AnimalMovements.v1.AnimalMovementMessage>().Create();
        var result = await client.PostAsync(endpoint,
            HttpContentUtility.CreateApplicationJsonAsStringContent(model),
            TestContext.Current.CancellationToken);

        result.IsSuccessStatusCode.Should().BeTrue();

        var responseBody = await result.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var responseData = JsonConvert.DeserializeObject<IngestionDto>(responseBody);

        responseData.Should().NotBeNull();
        responseData.IngestionId.Should().Contain(nation.ToString().ToLower());
        responseData.RecordCount.Should().Be(1);

        var s3FileExists = await CheckIfS3FileExistsAsync(responseData.IngestionId);
        s3FileExists.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidAnimalMovements_WhenPostingToEndpointV2_ShouldSucceed()
    {
        var nation = Nation.Wales;
        var endpoint = string.Format(TestEndpointConstants.AnimalMovementIngestionEndpointV2, nation);
        var client = apiContainerFixture.CreateBasicClient();

        var model = _fixture.Build<Controllers.AnimalMovements.v2.EventMessage>().Create();
        var result = await client.PostAsync(endpoint,
            HttpContentUtility.CreateApplicationJsonAsStringContent(model),
            TestContext.Current.CancellationToken);

        result.IsSuccessStatusCode.Should().BeTrue();

        var responseBody = await result.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var responseData = JsonConvert.DeserializeObject<IngestionDto>(responseBody);

        responseData.Should().NotBeNull();
        responseData.IngestionId.Should().Contain(nation.ToString().ToLower());
        responseData.RecordCount.Should().Be(1);

        var s3FileExists = await CheckIfS3FileExistsAsync(responseData.IngestionId);
        s3FileExists.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidAnimalMovements_WhenPostingToEndpointV1_ShouldStoreEncryptedFile()
    {
        var nation = Nation.Wales;
        var endpoint = string.Format(
            TestEndpointConstants.AnimalMovementIngestionEndpointV1,
            nation);

        var client = apiContainerFixture.CreateBasicClient();

        var model = _fixture.Build<Controllers.AnimalMovements.v1.AnimalMovementMessage>().Create();
        var result = await client.PostAsync(
            endpoint,
            HttpContentUtility.CreateApplicationJsonAsStringContent(model),
            TestContext.Current.CancellationToken);

        result.IsSuccessStatusCode.Should().BeTrue();

        var responseBody = await result.Content.ReadAsStringAsync(
            TestContext.Current.CancellationToken);

        var responseData = JsonConvert.DeserializeObject<IngestionDto>(responseBody);

        responseData.Should().NotBeNull();
        responseData.IngestionId.Should().Contain(nation.ToString().ToLowerInvariant());
        responseData.RecordCount.Should().Be(1);

        var decryptedPayload = await DownloadAndDecryptAsync(responseData.IngestionId);

        decryptedPayload.Should().Contain($"\"movementID\": \"{model.AnimalMovement.MovementID}\"");
    }

    [Fact]
    public async Task GivenValidAnimalMovements_WhenPostingToEndpointV2_ShouldStoreEncryptedFile()
    {
        var nation = Nation.Wales;
        var endpoint = string.Format(
            TestEndpointConstants.AnimalMovementIngestionEndpointV2,
            nation);

        var client = apiContainerFixture.CreateBasicClient();

        var model = _fixture.Build<Controllers.AnimalMovements.v2.EventMessage>().Create();
        var result = await client.PostAsync(
            endpoint,
            HttpContentUtility.CreateApplicationJsonAsStringContent(model),
            TestContext.Current.CancellationToken);

        result.IsSuccessStatusCode.Should().BeTrue();

        var responseBody = await result.Content.ReadAsStringAsync(
            TestContext.Current.CancellationToken);

        var responseData = JsonConvert.DeserializeObject<IngestionDto>(responseBody);

        responseData.Should().NotBeNull();
        responseData.IngestionId.Should().Contain(nation.ToString().ToLowerInvariant());
        responseData.RecordCount.Should().Be(1);

        var decryptedPayload = await DownloadAndDecryptAsync(responseData.IngestionId);

        decryptedPayload.Should().Contain($"\"recordID\": \"{model.Event.RecordID}\"");
    }

    private async Task<string> DownloadAndDecryptAsync(string key)
    {
        using var s3Object = await apiContainerFixture.LocalStackFixture.S3Client
            .GetObjectAsync(
                LocalStackFixture.CadsInternalBucketName,
                key,
                TestContext.Current.CancellationToken);

        await using var encryptedStream = new MemoryStream();
        await s3Object.ResponseStream.CopyToAsync(
            encryptedStream,
            TestContext.Current.CancellationToken);

        var encryptedBytes = encryptedStream.ToArray();
        encryptedStream.Position = 0;

        await using var decryptedStream = new MemoryStream();

        var encryptor = new FileEncryptor(
            new InMemoryMasterKeyProvider(
                TestEncryptionKeyProvider.CurrentKeyId,
                TestEncryptionKeyProvider.Keys));

        await encryptor.DecryptAsync(
            encryptedStream,
            decryptedStream,
            cancellationToken: TestContext.Current.CancellationToken);

        return Encoding.UTF8.GetString(decryptedStream.ToArray());
    }

    private async Task<bool> CheckIfS3FileExistsAsync(string key)
    {
        using var s3Object = await apiContainerFixture.LocalStackFixture.S3Client.GetObjectAsync(
            LocalStackFixture.CadsInternalBucketName,
            key,
            TestContext.Current.CancellationToken);

        using var reader = new StreamReader(s3Object.ResponseStream);
        var storedPayload = await reader.ReadToEndAsync(
            TestContext.Current.CancellationToken);

        return !string.IsNullOrEmpty(storedPayload);
    }
}