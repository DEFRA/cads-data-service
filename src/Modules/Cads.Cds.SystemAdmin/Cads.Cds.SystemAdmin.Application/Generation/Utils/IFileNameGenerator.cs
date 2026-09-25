using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;

namespace Cads.Cds.SystemAdmin.Application.Generation.Utils;

public interface IFileNameGenerator
{
    string Create(string app, string env, ImportActionType type, string batchId, string tableName, DateTime dateTime);

    string Create(string prefix, string app, string env, ImportActionType type, string batchId, string? partNo, string tableName, string timestamp, string extension);
}