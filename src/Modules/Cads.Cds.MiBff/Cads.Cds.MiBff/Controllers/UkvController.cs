using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Attributes;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.MiBff.Application.Queries.Ukv.Animals;
using Cads.Cds.MiBff.Application.Queries.Ukv.Audit;
using Cads.Cds.MiBff.Application.Queries.Ukv.Cohorts;
using Cads.Cds.MiBff.Application.Queries.Ukv.DataQuality;
using Cads.Cds.MiBff.Application.Queries.Ukv.Holdings;
using Cads.Cds.MiBff.Application.Queries.Ukv.Inspections;
using Cads.Cds.MiBff.Application.Queries.Ukv.JourneyHauliers;
using Cads.Cds.MiBff.Application.Queries.Ukv.Movements;
using Cads.Cds.MiBff.Application.Queries.Ukv.Zones;
using Cads.Cds.MiBff.Controllers.Requests.Ukv;
using Cads.Cds.MiBff.Core.DTOs.Ukv;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers;

[Authorize(Policy = AuthenticationConstants.AadReportsReadPolicy)]
[ApiController]
[Route("api/v1/bff/mi/[controller]")]
public class UkvController(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [ApiMessage($"Message text from {nameof(GetAnimals)} endpoint", $"Description text from {nameof(GetAnimals)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<UkvDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("animals")]
    public async Task<IActionResult> GetAnimals([FromQuery] GetAnimalsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetAnimalsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetAnimalsByAnimalId)} endpoint", $"Description text from {nameof(GetAnimalsByAnimalId)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<UkvDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("animals/{animalId}")]
    public async Task<IActionResult> GetAnimalsByAnimalId([FromRoute] Guid animalId)
    {
        var query = new GetAnimalsByAnimalIdQuery(animalId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetAuditsScrapie)} endpoint", $"Description text from {nameof(GetAuditsScrapie)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<UkvDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("audits/scrapie")]
    public async Task<IActionResult> GetAuditsScrapie([FromQuery] GetAuditsScrapiePagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetAuditScrapieQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetCohorts)} endpoint", $"Description text from {nameof(GetCohorts)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<UkvDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("cohorts")]
    public async Task<IActionResult> GetCohorts([FromQuery] GetCohortsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetCohortsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetCohortsByAnimalId)} endpoint", $"Description text from {nameof(GetCohortsByAnimalId)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<UkvDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("cohorts/{animalId}")]
    public async Task<IActionResult> GetCohortsByAnimalId([FromRoute] Guid animalId)
    {
        var query = new GetCohortsByAnimalIdQuery(animalId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetDataQualityUnregistered)} endpoint", $"Description text from {nameof(GetDataQualityUnregistered)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<UkvDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("data-quality/unregistered")]
    public async Task<IActionResult> GetDataQualityUnregistered([FromQuery] GetDataQualityUnregisteredPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetDataQualityUnregisteredQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetHoldings)} endpoint", $"Description text from {nameof(GetHoldings)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<HoldingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("holdings")]
    public async Task<IActionResult> GetHoldings([FromQuery] GetHoldingsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetHoldingsQuery, HoldingDto>(request);
        query.LastModified = request.LastModified;

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetHoldingsByCph)} endpoint", $"Description text from {nameof(GetHoldingsByCph)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<HoldingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("holdings/{cph}")]
    public async Task<IActionResult> GetHoldingsByCph([FromRoute] string cph)
    {
        var query = new GetHoldingsByCphQuery(cph);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetInspectionsSheepGoat)} endpoint", $"Description text from {nameof(GetInspectionsSheepGoat)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<UkvDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("inspections/sheep-goat")]
    public async Task<IActionResult> GetInspectionsSheepGoat([FromQuery] GetInspectionsSheepGoatPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetInspectionsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetJourneyHauliers)} endpoint", $"Description text from {nameof(GetJourneyHauliers)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<UkvDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("journeys/hauliers")]
    public async Task<IActionResult> GetJourneyHauliers([FromQuery] GetJourneyHauliersPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetJourneyHauliersQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetMovements)} endpoint", $"Description text from {nameof(GetMovements)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<UkvDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("movements")]
    public async Task<IActionResult> GetMovements([FromQuery] GetMovementsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetMovementsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetZones)} endpoint", $"Description text from {nameof(GetZones)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<UkvDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("zones")]
    public async Task<IActionResult> GetZones([FromQuery] GetZonesPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetZonesQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetZonesByZoneId)} endpoint", $"Description text from {nameof(GetZonesByZoneId)} endpoint")]
    [ResponseWithMetaData]
    [ProducesResponseType(typeof(PaginatedResult<UkvDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [HttpGet("zones/{zoneId}")]
    public async Task<IActionResult> GetZonesByZoneId([FromRoute] Guid zoneId)
    {
        var query = new GetZonesByZoneIdQuery(zoneId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }
}