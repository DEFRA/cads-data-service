using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Metrics;

[ExcludeFromCodeCoverage]
public static class S3ImportMetrics
{
    public static (Counter<int> counter, Histogram<double> fileHistogram, Histogram<double> batchHistogram) CreateBulkLoadMetrics()
    {
        var meter = new Meter("Cads.Cds.StorageBridge.BulkLoad.Postgres.Metrics", "1.0");

        var counter = meter.CreateCounter<int>("cads_batch_import_rows_affected", "rows");
        var fileHistogram = meter.CreateHistogram<double>("postgres_file_import_duration_ms", "ms");
        var batchHistogram = meter.CreateHistogram<double>("postgres_batch_import_duration_ms", "ms");

        return (counter, fileHistogram, batchHistogram);
    }
}