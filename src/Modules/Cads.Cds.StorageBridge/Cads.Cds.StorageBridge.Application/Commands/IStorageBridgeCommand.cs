using Cads.Cds.BuildingBlocks.Application.Commands;

namespace Cads.Cds.StorageBridge.Application.Commands;

public interface IStorageBridgeCommand<out TResponse> : ICommand<TResponse> { }