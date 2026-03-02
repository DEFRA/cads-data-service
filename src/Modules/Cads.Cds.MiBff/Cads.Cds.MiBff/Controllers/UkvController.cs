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

    [ResponseWithMetaData]
    [HttpGet("holdings")]
    public async Task<IActionResult> GetHoldings([FromQuery] GetHoldingsRequest request)
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

    [ResponseWithMetaData]
    [HttpGet("holdings/{cph}")]
    public async Task<IActionResult> GetHoldingsByCph([FromRoute] string cph)
    {
        var query = new GetHoldingsByCphQuery(cph);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ResponseWithMetaData]
    [HttpGet("animals/{animalId}")]
    public async Task<IActionResult> GetAnimalsByAnimalId(GetAnimalsRequest request)
    {
        var query = new GetAnimalsByAnimalIdQuery(request.AnimalId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ResponseWithMetaData]
    [HttpGet("movements")]
    public async Task<IActionResult> GetMovements(GetMovementsRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetMovementsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ResponseWithMetaData]
    [HttpGet("journeys/hauliers")]
    public async Task<IActionResult> GetJourneyHauliers(GetJourneyHauliersRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetJourneyHauliersQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ResponseWithMetaData]
    [HttpGet("zones/{zoneId}")]
    public async Task<IActionResult> GetZonesByZoneId(GetZonesRequest request)
    {
        var query = new GetZonesByZoneIdQuery(request.ZoneId);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ResponseWithMetaData]
    [HttpGet("cohorts/{animalsId}")]
    public async Task<IActionResult> GetCohortsByAnimalId(GetCohortsRequest request)
    {
        var query = new GetCohortsByAnimalIdQuery(request.AnimalId);
        
        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ResponseWithMetaData]
    [HttpGet("inspections/sheep-goat")]
    public async Task<IActionResult> GetInspectionsSheepGoat(GetInspectionsSheepGoatRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetInspectionsQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ResponseWithMetaData]
    [HttpGet("data-quality/unregistered")]
    public async Task<IActionResult> GetDataQualityUnregistered(GetDataQualityUnregisteredRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetDataQualityUnregisteredQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [ResponseWithMetaData]
    [HttpGet("audits/scrapie")]
    public async Task<IActionResult> GetAuditsScrapie(GetAuditsScrapieRequest request)
    {
        var query = QueryFactory.CreatePagedQuery<GetAuditScrapieQuery, UkvDto>(request);

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }
}