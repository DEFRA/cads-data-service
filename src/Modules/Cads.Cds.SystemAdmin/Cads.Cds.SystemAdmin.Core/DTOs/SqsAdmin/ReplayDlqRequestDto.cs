namespace Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin;

public record ReplayDlqRequestDto(string Queue, int BatchSize);

