using Cads.Cds.Api.Application.Soap.Messages;
using CoreWCF;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.Api.Application.Soap.ServiceContracts;

/// <summary>
/// Implementation of ICattleStatusService
/// </summary>
public class CattleStatusService : ICattleStatusService
{
    private readonly ILogger<CattleStatusService> _logger;

    public CattleStatusService(ILogger<CattleStatusService> logger)
    {
        _logger = logger;
    }

    public async Task<GetCattleStatusResponse> GetCattleStatusRequest(string? HoldingId)
    {
        if (string.IsNullOrWhiteSpace(HoldingId))
        {
            throw new FaultException($"Holding id cannot be null or whitespace");
        }

        try
        {
            var response = new GetCattleStatusResponse
            {
                HoldingId = HoldingId,
                CattleStatusCSV = await GetCattleStatusData()
            };

            _logger.LogInformation("Successfully processed GetCattleStatusRequest for HoldingId: {HoldingId}", HoldingId);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing GetCattleStatusRequest for HoldingId: {HoldingId}", HoldingId);

            // Throw SOAP fault
            throw new FaultException($"Error processing request: {ex.Message}");
        }
    }

    private async Task<string> GetCattleStatusData()
    {
        // TODO: Implement actual data retrieval logic
        // Mock CSV data matching the expected format
        return """
               CHD2026007WEP09461,H,14:37:41,07/01/2026,CHD
               CHD2026007WEP09461,A,1,UK160573102366,HF,F,08/04/2022,UK160573101435,,,11/09/2024,,,,,,,,,,,,,,,,,,,,
               CHD2026007WEP09461,A,2,UK160573302368,HF,F,06/04/2022,UK160573101694,,,22/08/2024,,,,,,,,,,,,,,,,,,,,
               CHD2026007WEP09461,A,3,UK160573402383,HF,F,18/05/2022,UK160573301885,,,22/08/2024,,,,,,,,,,,,,,,,,,,,
               CHD2026007WEP09461,A,4,UK202605605466,HF,F,15/05/2022,UK202605704669,,X XXX XXXXXXXX,31/12/2024,,,,,,,,,,,,,,,,,,,,
               CHD2026007WEP09461,A,5,UK300088201719,HF,F,21/10/2021,UK300088301251,,CB0137,14/08/2024,,,,,,,,,,,,,,,,,,,,
               """;
    }
}