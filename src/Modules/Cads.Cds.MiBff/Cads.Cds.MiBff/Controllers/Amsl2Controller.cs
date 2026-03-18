using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Attributes;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.MiBff.Application.Queries.Amsl2.AnimalSummary;
using Cads.Cds.MiBff.Application.Queries.Amsl2.AnnualInventory;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DepartureDetails;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DestinationDetails;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DetailedMovements;
using Cads.Cds.MiBff.Application.Queries.Amsl2.MovementsInSuspense;
using Cads.Cds.MiBff.Application.Queries.Amsl2.SummaryPremiseDetails;
using Cads.Cds.MiBff.Controllers.Requests.Amsl2;
using Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers;

[Authorize(Policy = AuthenticationConstants.AadReportsReadPolicy)]
[ApiController]
[Route("api/v1/bff/mi/[controller]")]
public class Amsl2Controller(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [ApiMessage($"Message text from {nameof(GetAnimalSummaryById)} endpoint", $"Description text from {nameof(GetAnimalSummaryById)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("animal-summary/{animalId}")]
    public async Task<IActionResult> GetAnimalSummaryById([FromRoute] Guid animalId)
    {
        var query = new GetAnimalSummaryByAnimalIdQuery(animalId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetAnnualInventory)} endpoint", $"Description text from {nameof(GetAnnualInventory)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("annual-inventory")]
    public async Task<IActionResult> GetAnnualInventory([FromQuery] GetAnnualInventoryPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetAnnualInventoryQuery, Amsl2Dto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetDepartureDetailsByCph)} endpoint", $"Description text from {nameof(GetDepartureDetailsByCph)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("departure-details/{cph}")]
    public async Task<IActionResult> GetDepartureDetailsByCph([FromRoute] string cph)
    {
        var query = new GetDepartureDetailsByCphQuery(cph);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetDestinationDetails)} endpoint", $"Description text from {nameof(GetDestinationDetails)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("destination-details/{destinationType}/{destinationId}")]
    public async Task<IActionResult> GetDestinationDetails([FromRoute] string destinationType, [FromRoute] Guid destinationId)
    {
        var query = new GetDestinationDetailsQuery(destinationType, destinationId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetDetailedMovementsById)} endpoint", $"Description text from {nameof(GetDetailedMovementsById)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("detailed-movement/{movementId}")]
    public async Task<IActionResult> GetDetailedMovementsById([FromRoute] Guid movementId)
    {
        var query = new GetDetailedMovementsByMovementIdQuery(movementId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetMovementsInSuspense)} endpoint", $"Description text from {nameof(GetMovementsInSuspense)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("movements-in-suspense")]
    public async Task<IActionResult> GetMovementsInSuspense([FromQuery] GetMovementsInSuspensePagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetMovementsInSuspenseQuery, Amsl2Dto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetSummaryPremiseDetails)} endpoint", $"Description text from {nameof(GetSummaryPremiseDetails)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("summary-premise-details")]
    public async Task<IActionResult> GetSummaryPremiseDetails([FromQuery] GetSummaryPremiseDetailsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetSummaryPremiseDetailsQuery, Amsl2Dto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }
}