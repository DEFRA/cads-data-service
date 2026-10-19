namespace Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin;

public record ReplayDlqMessageRequestDto(string Queue, string MessageId);

