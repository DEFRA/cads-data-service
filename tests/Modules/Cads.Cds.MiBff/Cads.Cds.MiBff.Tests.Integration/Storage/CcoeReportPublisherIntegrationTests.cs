using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.MiBff.Infrastructure.Storage.Clients;
using Cads.Cds.MiBff.Infrastructure.Storage.Configuration;
using Cads.Cds.MiBff.Infrastructure.Storage.Services;
using FluentAssertions;
using System.Text;

namespace Cads.Cds.MiBff.Tests.Integration.Storage;

[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class CcoeReportPublisherIntegrationTests(ApiContainerFixture apiContainerFixture)
{
    private CcoeReportPublisher CreateSut()
    {
        var factory = new S3ClientFactory();

        // Reuse the already-running LocalStack container's real S3 client (not a mock) so this
        // exercises genuine upload + SigV4 presigning behaviour end-to-end.
        factory.RegisterMockClient<CcoeReportStorageClient>(
            LocalStackFixture.CcoeReportsBucketName,
            healthCheckEnabled: false,
            apiContainerFixture.LocalStackFixture.S3Client);

        var storageConfig = new MiBffStorageConfiguration
        {
            Ccoe = new CcoeStorageConfigurationDetails
            {
                BucketName = LocalStackFixture.CcoeReportsBucketName,
                PresignedUrlExpiryMinutes = 15
            }
        };

        return new CcoeReportPublisher(factory, storageConfig);
    }

    [Fact]
    public async Task GivenNewReport_WhenPublished_UploadsToRealBucketAndReturnsWorkingPresignedUrl()
    {
        var sut = CreateSut();
        var reportKey = $"integration-test-{Guid.NewGuid():N}";
        var jobId = Guid.NewGuid();
        var content = Encoding.UTF8.GetBytes($"fake-xlsx-content-{Guid.NewGuid()}");

        var link = await sut.PublishXlsxAsync(
            reportKey, jobId, new MemoryStream(content), TestContext.Current.CancellationToken);

        link.Url.Should().NotBeNullOrEmpty();
        link.ExpiresAt.Should().BeAfter(DateTimeOffset.UtcNow);

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(link.Url, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();

        var downloaded = await response.Content.ReadAsByteArrayAsync(TestContext.Current.CancellationToken);
        downloaded.Should().Equal(content);
        response.Content.Headers.ContentDisposition?.DispositionType.Should().Be("attachment");
    }

    [Fact]
    public async Task GivenReportAlreadyPublished_WhenPublishedAgain_SkipsReuploadButStillReturnsWorkingPresignedUrl()
    {
        var sut = CreateSut();
        var reportKey = $"integration-test-{Guid.NewGuid():N}";
        var jobId = Guid.NewGuid();
        var originalContent = Encoding.UTF8.GetBytes("original-content");
        var differentContent = Encoding.UTF8.GetBytes("different-content-that-should-not-overwrite");

        await sut.PublishXlsxAsync(reportKey, jobId, new MemoryStream(originalContent), TestContext.Current.CancellationToken);
        var secondLink = await sut.PublishXlsxAsync(reportKey, jobId, new MemoryStream(differentContent), TestContext.Current.CancellationToken);

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(secondLink.Url, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();

        var downloaded = await response.Content.ReadAsByteArrayAsync(TestContext.Current.CancellationToken);
        downloaded.Should().Equal(
            originalContent,
            because: "the existing object must not be overwritten on a second publish for the same report/job");
    }
}