namespace Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin;

public record QueueInfoDto(string Name, string QueueUrl, string? DlqQueueUrl);