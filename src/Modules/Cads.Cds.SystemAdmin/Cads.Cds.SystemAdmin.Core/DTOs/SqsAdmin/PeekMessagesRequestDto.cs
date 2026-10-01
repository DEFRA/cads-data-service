namespace Cads.Cds.SystemAdmin.Core.DTOs.SqsAdmin;

public record PeekMessagesRequestDto(string Queue, int MaxMessages);