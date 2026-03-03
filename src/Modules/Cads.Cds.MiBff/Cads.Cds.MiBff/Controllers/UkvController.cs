using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Attributes;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Application.Queries.Animals;
using Cads.Cds.MiBff.Application.Queries.Audit;
using Cads.Cds.MiBff.Application.Queries.Cohorts;
using Cads.Cds.MiBff.Application.Queries.DataQuality;
using Cads.Cds.MiBff.Application.Queries.Holdings;
using Cads.Cds.MiBff.Application.Queries.Inspections;
using Cads.Cds.MiBff.Application.Queries.JourneyHauliers;
using Cads.Cds.MiBff.Application.Queries.Movements;
using Cads.Cds.MiBff.Application.Queries.Zones;
using Cads.Cds.MiBff.Controllers.Requests;
using Cads.Cds.MiBff.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers;

[ApiController]
[Route("api/v1/bff/[controller]")]
public class UkvController(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [ApiMessage($"Message text from {nameof(GetAnimals)} endpoint", $"Description text from {nameof(GetAnimals)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("animals")]
    public async Task<IActionResult> GetAnimals([FromQuery] GetAnimalsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetAnimalsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetAnimalsByAnimalId)} endpoint", $"Description text from {nameof(GetAnimalsByAnimalId)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("animals/{animalId}")]
    public async Task<IActionResult> GetAnimalsByAnimalId([FromRoute] Guid animalId)
    {
        var query = new GetAnimalsByAnimalIdQuery(animalId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetAuditsScrapie)} endpoint", $"Description text from {nameof(GetAuditsScrapie)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("audits/scrapie")]
    public async Task<IActionResult> GetAuditsScrapie([FromQuery] GetAuditsScrapiePagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetAuditScrapieQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetCohorts)} endpoint", $"Description text from {nameof(GetCohorts)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("cohorts")]
    public async Task<IActionResult> GetCohorts([FromQuery] GetCohortsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetCohortsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetCohortsByAnimalId)} endpoint", $"Description text from {nameof(GetCohortsByAnimalId)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("cohorts/{animalId}")]
    public async Task<IActionResult> GetCohortsByAnimalId([FromRoute] Guid animalId)
    {
        var query = new GetCohortsByAnimalIdQuery(animalId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetDataQualityUnregistered)} endpoint", $"Description text from {nameof(GetDataQualityUnregistered)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("data-quality/unregistered")]
    public async Task<IActionResult> GetDataQualityUnregistered([FromQuery] GetDataQualityUnregisteredPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetDataQualityUnregisteredQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetHoldings)} endpoint", $"Description text from {nameof(GetHoldings)} endpoint")]
    [ResponseWithMetaData]
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
    [HttpGet("holdings/{cph}")]
    public async Task<IActionResult> GetHoldingsByCph([FromQuery] GetHoldingsRequest request)
    {
        var query = new GetHoldingsByCphQuery(request.Cph);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetInspectionsSheepGoat)} endpoint", $"Description text from {nameof(GetInspectionsSheepGoat)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("inspections/sheep-goat")]
    public async Task<IActionResult> GetInspectionsSheepGoat([FromQuery] GetInspectionsSheepGoatPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetInspectionsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetJourneyHauliers)} endpoint", $"Description text from {nameof(GetJourneyHauliers)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("journeys/hauliers")]
    public async Task<IActionResult> GetJourneyHauliers([FromQuery] GetJourneyHauliersPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetJourneyHauliersQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetMovements)} endpoint", $"Description text from {nameof(GetMovements)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("movements")]
    public async Task<IActionResult> GetMovements([FromQuery] GetMovementsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetMovementsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetZones)} endpoint", $"Description text from {nameof(GetZones)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("zones")]
    public async Task<IActionResult> GetZones([FromQuery] GetZonesPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetZonesQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetZonesByZoneId)} endpoint", $"Description text from {nameof(GetZonesByZoneId)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("zones/{zoneId}")]
    public async Task<IActionResult> GetZonesByZoneId([FromRoute] Guid zoneId)
    {
        var query = new GetZonesByZoneIdQuery(zoneId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }
}