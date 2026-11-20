using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Reports.Jobs;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Testing.Support.Fakes.Repositories;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetReportJobFileCcoeTests
{
    private const string PresignedUrl = "https://ccoe.example.test/signed-url";

    private static string Endpoint(string reportKey, Guid jobId) =>
        $"/api/v1/bff/mi/reports/{reportKey}/job/{jobId}?fileType=xlsx";

    private static string ExpectedCcoeKey(string reportKey, Guid jobId) =>
        $"reports/{reportKey}/{jobId}/{reportKey}.xlsx";

    /// <summary>
    /// Wires up the shared AmazonS3Mock so it behaves like a real bucket for both the internal
    /// (encrypted JSON) store and the CCoE (xlsx) store: PutObjectAsync records bytes by key,
    /// GetObjectAsync replays them back, and GetObjectMetadataAsync only succeeds for keys that
    /// have actually been uploaded (so CcoeReportPublisher's exists-check behaves correctly).
    /// </summary>
    private static void WireUpS3(MiBffWebApplicationFactory factory, Dictionary<string, byte[]> objects)
    {
        factory.AmazonS3Mock
            .Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .Callback((PutObjectRequest request, CancellationToken _) =>
            {
                using var copy = new MemoryStream();
                request.InputStream.CopyTo(copy);
                objects[request.Key] = copy.ToArray();
            })
            .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK });

        factory.AmazonS3Mock
            .Setup(s => s.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetObjectRequest request, CancellationToken _) => new GetObjectResponse
            {
                HttpStatusCode = HttpStatusCode.OK,
                ResponseStream = new MemoryStream(objects[request.Key])
            });

        factory.AmazonS3Mock
            .Setup(s => s.GetObjectMetadataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns((string _, string key, CancellationToken _) => objects.ContainsKey(key)
                ? Task.FromResult(new GetObjectMetadataResponse())
                : Task.FromException<GetObjectMetadataResponse>(
                    new AmazonS3Exception("not found") { StatusCode = HttpStatusCode.NotFound }));

        factory.AmazonS3Mock
            .Setup(s => s.GetPreSignedURL(It.IsAny<GetPreSignedUrlRequest>()))
            .Returns(PresignedUrl);
    }

    private static async Task<Guid> SeedCompletedJobAsync(MiBffWebApplicationFactory factory)
    {
        await using var scope = factory.Services.CreateAsyncScope();
        var repository = scope.ServiceProvider.GetRequiredService<FakeMiReportJobRepository>();
        var processor = scope.ServiceProvider.GetRequiredService<IReportJobProcessor>();

        var jobId = Guid.NewGuid();
        await repository.CreateJobWithOutboxAsync(
            new MiReportJob
            {
                ReportJobId = jobId,
                ReportKey = GetGbCattleDeathsQuery.ExpectedKey,
                ExternalSubject = "user@example.test",
                RequestPayload = "{\"reportKey\":\"gb_cattle_deaths\",\"year\":2026}",
                Status = ReportJobStatus.Pending
            },
            new MiReportJobOutbox { OutboxId = Guid.NewGuid(), ReportJobId = jobId, Subject = "ReportJobRequested", Payload = "{}" },
            TestContext.Current.CancellationToken);

        await processor.ProcessAsync(jobId, TestContext.Current.CancellationToken);

        return jobId;
    }

    [Fact]
    public async Task GivenCompletedJob_WhenXlsxRequested_UploadsToCcoeBucketAndReturnsPresignedDownloadUrl()
    {
        await using var factory = new MiBffWebApplicationFactory(useFakeAuth: true);
        using var client = factory.CreateClient();
        client.AddJwt();

        var objects = new Dictionary<string, byte[]>();
        WireUpS3(factory, objects);

        var jobId = await SeedCompletedJobAsync(factory);
        var reportKey = GetGbCattleDeathsQuery.ExpectedKey;

        var response = await client.GetAsync(Endpoint(reportKey, jobId), TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<RedirectResponseDto>(TestContext.Current.CancellationToken);
        body.Should().NotBeNull();
        body!.DownloadUrl.Should().Be(PresignedUrl);
        body.ExpiresAt.Should().BeAfter(DateTimeOffset.UtcNow);

        var ccoeKey = ExpectedCcoeKey(reportKey, jobId);
        objects.Should().ContainKey(ccoeKey);

        factory.AmazonS3Mock.Verify(s => s.PutObjectAsync(
            It.Is<PutObjectRequest>(r => r.Key == ccoeKey && r.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
            It.IsAny<CancellationToken>()), Times.Once);

        factory.AmazonS3Mock.Verify(s => s.GetPreSignedURL(
            It.Is<GetPreSignedUrlRequest>(r => r.Key == ccoeKey && r.Verb == HttpVerb.GET)), Times.Once);
    }

    [Fact]
    public async Task GivenJobAlreadyPublishedToCcoe_WhenXlsxRequestedAgain_SkipsReuploadButStillReturnsPresignedUrl()
    {
        await using var factory = new MiBffWebApplicationFactory(useFakeAuth: true);
        using var client = factory.CreateClient();
        client.AddJwt();

        var objects = new Dictionary<string, byte[]>();
        WireUpS3(factory, objects);

        var jobId = await SeedCompletedJobAsync(factory);
        var reportKey = GetGbCattleDeathsQuery.ExpectedKey;
        var endpoint = Endpoint(reportKey, jobId);

        var first = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        first.StatusCode.Should().Be(HttpStatusCode.OK);

        var second = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        second.StatusCode.Should().Be(HttpStatusCode.OK);

        var ccoeKey = ExpectedCcoeKey(reportKey, jobId);

        factory.AmazonS3Mock.Verify(s => s.PutObjectAsync(
            It.Is<PutObjectRequest>(r => r.Key == ccoeKey),
            It.IsAny<CancellationToken>()), Times.Once,
            "the second request should detect the existing object via HEAD and skip re-uploading");

        factory.AmazonS3Mock.Verify(s => s.GetPreSignedURL(
            It.Is<GetPreSignedUrlRequest>(r => r.Key == ccoeKey)), Times.Exactly(2),
            "a fresh presigned URL should still be issued on every request");
    }

    private record RedirectResponseDto(string DownloadUrl, DateTimeOffset ExpiresAt);
}