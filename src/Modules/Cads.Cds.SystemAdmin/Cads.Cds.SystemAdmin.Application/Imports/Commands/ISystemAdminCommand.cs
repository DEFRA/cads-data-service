using Cads.Cds.BuildingBlocks.Application.Commands;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands;

public interface ISystemAdminCommand<out TResponse> : ICommand<TResponse> { }