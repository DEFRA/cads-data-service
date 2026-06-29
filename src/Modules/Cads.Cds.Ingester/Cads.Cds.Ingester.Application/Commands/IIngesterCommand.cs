using Cads.Cds.BuildingBlocks.Application.Commands;

namespace Cads.Cds.Ingester.Application.Commands;

public interface IIngesterCommand<TResponse> : ICommand<TResponse> { }
