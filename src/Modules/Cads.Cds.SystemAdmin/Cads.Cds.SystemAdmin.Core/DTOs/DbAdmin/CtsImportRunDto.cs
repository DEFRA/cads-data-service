namespace Cads.Cds.SystemAdmin.Core.DTOs.DbAdmin;

public record CtsImportRunDto(
    long RunId,
    string Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset? BulkCompletedAt,
    DateTimeOffset? CompletedAt);
