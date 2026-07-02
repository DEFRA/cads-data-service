using Cads.Cds.BuildingBlocks.Application.Commands;

namespace Cads.Cds.Api.Application.Commands;

public interface IApiCommand<out TResponse> : ICommand<TResponse> { }