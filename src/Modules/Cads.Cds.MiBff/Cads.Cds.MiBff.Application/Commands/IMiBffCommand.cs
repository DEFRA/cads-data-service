using Cads.Cds.BuildingBlocks.Application.Commands;

namespace Cads.Cds.MiBff.Application.Commands;

public interface IMiBffCommand<out TResponse> : ICommand<TResponse> { }