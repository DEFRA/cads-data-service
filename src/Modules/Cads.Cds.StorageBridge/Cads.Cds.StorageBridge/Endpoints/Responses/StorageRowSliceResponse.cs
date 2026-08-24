namespace Cads.Cds.StorageBridge.Endpoints.Responses;

public record StorageRowSliceResponse(IReadOnlyList<string> Rows, bool ReachedEnd);
