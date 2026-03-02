using Cads.Cds.Api.Application.Soap;
using Microsoft.Extensions.Logging;
using CoreWCF;

namespace Cads.Cds.Api.Application.Services;

/// <summary>
/// Implementation of ILivestockMovementsService
/// </summary>
public class LivestockMovementsService : ILivestockMovementsService
{
    private readonly ILogger<LivestockMovementsService> _logger;
    private readonly SoapHeaderService _soapHeaderService;

    public LivestockMovementsService(ILogger<LivestockMovementsService> logger, SoapHeaderService soapHeaderService)
    {
        _logger = logger;
        _soapHeaderService = soapHeaderService;
    }

    public async Task<GetAnimalCohortResponse> GetAnimalCohortRequest(GetAnimalCohortRequest request)
    {
        _logger.LogInformation("GetAnimalCohortRequest method invoked - Request is null: {RequestNull}", request == null);

        if (request == null)
        {
            _logger.LogWarning("Request is null - CoreWCF deserialization failed.");
            throw new FaultException("Request cannot be null.");
        }

        _logger.LogInformation("Request received - ServiceOptions is null: {ServiceOptionsNull}, AnimalCohortQuery is null: {QueryNull}",
            request.ServiceOptions == null, request.AnimalCohortQuery == null);

        if (request.ServiceOptions != null)
        {
            _logger.LogInformation("ServiceOptions data - DbName: {DbName}, StoredProc: {StoredProc}",
                request.ServiceOptions.DestinationDataBaseName, request.ServiceOptions.DestinationStoredProcedure);
        }

        // Extract SOAP headers for authentication and correlation
        var (systemUser, endUser, clientRequestId) = _soapHeaderService.ExtractHeaders();

        _logger.LogInformation(
            "SOAP Headers - SystemUser: {SystemUser}, EndUser: {EndUser}, ClientRequestId: {ClientRequestId}",
            systemUser ?? "null",
            endUser ?? "null",
            clientRequestId ?? "null");

        try
        {
            // TODO: Implement actual business logic here
            // This could call a repository, query handler, or external service
            // For now, returning a mock response

            var response = new GetAnimalCohortResponse
            {
                CohortAnimals = await GetMockCohortData(),
                TraceIdentifier = request.AnimalCohortQuery?.TraceIdentifier
            };

            _logger.LogInformation("Successfully processed GetAnimalCohortRequest");

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing GetAnimalCohortRequest");

            // Throw SOAP fault
            throw new FaultException($"Error processing request: {ex.Message}");
        }
    }

    private async Task<string> GetMockCohortData()
    {
        // TODO: Implement actual data retrieval logic
        await Task.CompletedTask;

        // Mock cohort data - replace with actual implementation
        return "MOCK_COHORT_DATA";
    }
}
