using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;

public class FileNameGenerator : IFileNameGenerator
{
    // Pattern 1: CTSM_<app>_<env>_<type>_<batchId>_<partno>_<tablename>_<timestamp>.csv
    // Pattern 2: CTSM_<app>_<env>_<type>_<batchId>_<tablename>_<timestamp>.csv

    public string Create(string app, string env, ImportActionType type, string batchId, string tableName, DateTime dateTime)
    {
        var timestamp = dateTime.ToString("yyyy-MM-dd-HHmss");
        return Create("CTSM", app, env, type, batchId, null, tableName, timestamp, ".csv");
    }

    public string Create(string prefix, string app, string env, ImportActionType type, string batchId, string? partNo, string tableName, string timestamp, string extension)
    {
        // Normalize extension to avoid double dots (caller may pass ".csv" or "csv")
        var ext = string.IsNullOrEmpty(extension) ? string.Empty : extension.TrimStart('.');

        // Include partNo when provided (follows Pattern 1); omit when empty (Pattern 2)
        if (!string.IsNullOrWhiteSpace(partNo))
        {
            return $"{prefix}_{app}_{env}_{type}_{batchId}_{partNo}_{tableName}_{timestamp}.{ext}";
        }

        return $"{prefix}_{app}_{env}_{type}_{batchId}_{tableName}_{timestamp}.{ext}".ToUpper();
    }
}