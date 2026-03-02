using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Attributes;
using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Application.Queries.Animals;
using Cads.Cds.MiBff.Application.Queries.Audits;
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
    public async Task<IActionResult> GetAnimals(GetAnimalsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetAnimalsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetAnimalsByAnimalId)} endpoint", $"Description text from {nameof(GetAnimalsByAnimalId)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("animals/{animalId}")]
    public async Task<IActionResult> GetAnimalsByAnimalId(GetAnimalsByAnimalIdRequest request)
    {
        var query = new GetAnimalsByAnimalIdQuery(request.AnimalId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetAuditsScrapie)} endpoint", $"Description text from {nameof(GetAuditsScrapie)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("audits/scrapie")]
    public async Task<IActionResult> GetAuditsScrapie(GetAuditsScrapiePagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetAuditScrapieQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetCohorts)} endpoint", $"Description text from {nameof(GetCohorts)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("cohorts")]
    public async Task<IActionResult> GetCohorts(GetCohortsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetCohortsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetCohortsByAnimalId)} endpoint", $"Description text from {nameof(GetCohortsByAnimalId)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("cohorts/{animalId}")]
    public async Task<IActionResult> GetCohortsByAnimalId(GetCohortsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetCohortsByAnimalIdQuery, UkvDto>(request);
        query.AnimalId = request.AnimalId;

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetDataQualityUnregistered)} endpoint", $"Description text from {nameof(GetDataQualityUnregistered)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("data-quality/unregistered")]
    public async Task<IActionResult> GetDataQualityUnregistered(GetDataQualityUnregisteredPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetDataQualityUnregisteredQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetHoldings)} endpoint", $"Description text from {nameof(GetHoldings)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("holdings")]
    public async Task<IActionResult> GetHoldings(GetHoldingsPagedRequest request)
    {
        var query = new GetHoldingsQuery
        {
            LastModified = request.LastModified,
            Order = request.Order,
            Sort = request.Sort,
            Page = request.Page ?? 1,
            PageSize = request.PageSize ?? 10
        };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetHoldingsByCph)} endpoint", $"Description text from {nameof(GetHoldingsByCph)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("holdings/{cph}")]
    public async Task<IActionResult> GetHoldingsByCph(GetHoldingsRequest request)
    {
        var query = new GetHoldingsByCphQuery(request.Cph);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetInspectionsSheepGoat)} endpoint", $"Description text from {nameof(GetInspectionsSheepGoat)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("inspections/sheep-goat")]
    public async Task<IActionResult> GetInspectionsSheepGoat(GetInspectionsSheepGoatPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetInspectionsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetJourneyHauliers)} endpoint", $"Description text from {nameof(GetJourneyHauliers)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("journeys/hauliers")]
    public async Task<IActionResult> GetJourneyHauliers(GetJourneyHauliersPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetJourneyHauliersQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetMovements)} endpoint", $"Description text from {nameof(GetMovements)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("movements")]
    public async Task<IActionResult> GetMovements(GetMovementsPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetMovementsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetZones)} endpoint", $"Description text from {nameof(GetZones)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("zones")]
    public async Task<IActionResult> GetZones(GetZonesPagedRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetZonesQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ApiMessage($"Message text from {nameof(GetZonesByZoneId)} endpoint", $"Description text from {nameof(GetZonesByZoneId)} endpoint")]
    [ResponseWithMetaData]
    [HttpGet("zones/{zoneId}")]
    public async Task<IActionResult> GetZonesByZoneId(GetZonesByZoneIdRequest request)
    {
        var query = new GetZonesByZoneIdQuery(request.ZoneId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }
}