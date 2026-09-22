using System.Globalization;
using Cads.Cds.BuildingBlocks.Application.Schema;

namespace Cads.Cds.BuildingBlocks.Application.Imports.Utilities;

public sealed class CtsmFilenameGenerator : ICtsmFilenameGenerator
{
    public const string App = "CADS";
    public const string Env = "ETE";
    public const string Extension = ".csv";
    public const string TimestampFormat = "yyyy-MM-dd-HHmmss";

    public const int MinBatchId = 1;
    public const int MaxBatchId = 99999;
    public const int MinPartNo = 1;
    public const int MaxPartNo = 999;

    private readonly TimeProvider _timeProvider;

    private int _lastBatchId;

    public CtsmFilenameGenerator()
        : this(TimeProvider.System)
    {
    }

    public CtsmFilenameGenerator(TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(timeProvider);

        _timeProvider = timeProvider;
    }

    public string Generate(CtsmFilenameRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.ReusePreviousFilename)
        {
            return Reissue(request.PreviousFilename);
        }

        var batchId = request.BatchId ?? NextBatchId();

        ArgumentOutOfRangeException.ThrowIfLessThan(batchId, MinBatchId, nameof(request.BatchId));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(batchId, MaxBatchId, nameof(request.BatchId));
        ArgumentOutOfRangeException.ThrowIfLessThan(request.PartNo, MinPartNo, nameof(request.PartNo));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(request.PartNo, MaxPartNo, nameof(request.PartNo));

        var schemaName = request.ActionType.GetSchemaName();
        var tableName = request.DataType.GetTableName(schemaName);

        if (string.IsNullOrWhiteSpace(tableName))
        {
            throw new ArgumentException($"No '{schemaName}' table name is defined for import data type '{request.DataType}'.", nameof(request));
        }

        var type = request.ActionType.ToString().ToUpperInvariant();
        var timestamp = _timeProvider.GetUtcNow().ToString(TimestampFormat, CultureInfo.InvariantCulture);

        return $"CTSM_{App}_{Env}_{type}_" +
               $"{batchId.ToString("D5", CultureInfo.InvariantCulture)}_" +
               $"{request.PartNo.ToString("D3", CultureInfo.InvariantCulture)}_" +
               $"{tableName.ToUpperInvariant()}_" +
               $"{timestamp}{Extension}";
    }

    private int NextBatchId()
    {
        var next = unchecked((uint)Interlocked.Increment(ref _lastBatchId));

        return (int)((next - 1) % MaxBatchId) + MinBatchId;
    }

    private static string Reissue(string? previousFilename)
    {
        if (string.IsNullOrWhiteSpace(previousFilename))
        {
            throw new ArgumentException("A previous filename is required when reusing a previously issued filename.", nameof(previousFilename));
        }

        if (!CtsmFilenameParser.TryParse(previousFilename, out _))
        {
            throw new FormatException($"Invalid CTSM filename format: {previousFilename}");
        }

        return previousFilename;
    }
}