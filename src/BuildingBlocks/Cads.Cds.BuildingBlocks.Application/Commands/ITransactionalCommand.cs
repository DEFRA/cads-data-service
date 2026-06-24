namespace Cads.Cds.BuildingBlocks.Application.Commands;

/// <summary>
/// Usage:
/// public sealed record CreateUserCommand(...) : ICommand<UserDto>, ITransactionalCommand;
/// public sealed record PublishAuditEventCommand(...) : ICommand<Unit>; // NOT transactional
/// </summary>
public interface ITransactionalCommand { }