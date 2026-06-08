namespace Cads.Cds.BuildingBlocks.Application.Identity;

public interface IUserContext
{
    string? Oid { get; }
    string? Email { get; }
    string? DisplayName { get; }
    string? TenantId { get; }
    string? UserIdentifier { get; }
}