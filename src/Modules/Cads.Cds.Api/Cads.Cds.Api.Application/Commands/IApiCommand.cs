using Cads.Cds.BuildingBlocks.Application.Commands;

namespace Cads.Cds.Api.Application.Commands;

public interface IApiCommand<TResponse> : ICommand<TResponse> { }
