using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Application.Encryption;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Reports.Jobs;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Testing.Support.Fakes.Repositories;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using System.Text.Json;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class ProcessReportJobTests
{
    [Fact]
    public async Task GivenPendingGbCattleDeathsJob_WhenProcessed_ThenEncryptedJsonIsStoredAndJobCompletes()
    {
        await using var factory = new MiBffWebApplicationFactory(useFakeAuth: true);
        _ = factory.CreateClient(); // start the host so S3 clients are configured

        byte[]? uploaded = null;
        PutObjectRequest? request = null;
        factory.AmazonS3Mock
            .Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
            .Callback((PutObjectRequest r, CancellationToken _) =>
            {
                request = r;
                using var copy = new MemoryStream();
                r.InputStream.CopyTo(copy);
                uploaded = copy.ToArray();
            })
            .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK });

        await using var scope = factory.Services.CreateAsyncScope();
        var repository = scope.ServiceProvider.GetRequiredService<FakeMiReportJobRepository>();
        var processor = scope.ServiceProvider.GetRequiredService<IReportJobProcessor>();
        var encryptor = scope.ServiceProvider.GetRequiredService<IFileEncryptor>();

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

        var job = await repository.GetByReportJobIdAsync(jobId, TestContext.Current.CancellationToken);
        job!.Status.Should().Be(ReportJobStatus.Complete);

        request.Should().NotBeNull();
        request!.Key.Should().Be($"mibff-reports/gbcattledeaths/{jobId}");

        // The stored object must not be plaintext JSON, but must decrypt back to it.
        uploaded.Should().NotBeNull();
        var act = () => JsonDocument.Parse(uploaded!);
        act.Should().Throw<JsonException>();

        using var decrypted = new MemoryStream();
        await encryptor.DecryptAsync(new MemoryStream(uploaded!), decrypted, cancellationToken: TestContext.Current.CancellationToken);
        decrypted.Position = 0;

        using var document = await JsonDocument.ParseAsync(decrypted, cancellationToken: TestContext.Current.CancellationToken);
        document.RootElement.ValueKind.Should().Be(JsonValueKind.Array);
    }
}
