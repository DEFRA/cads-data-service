using Cads.Cds.MiBff.Application.Queries.Holdings;
using Cads.Cds.MiBff.Application.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Application.Controllers;

[ApiController] 
[Route("api/v1/bff/[controller]")]
public class UkvController(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

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

    [HttpGet("holdings/{cph}")]
    public async Task<IActionResult> GetHoldingsByCph([FromRoute] string cph)
    {
        var query = new GetHoldingsByCphQuery(cph);
        
        var result = await _executor.ExecuteQuery(query);

        if (result.Count > 0)
            return Ok(result);
        else
            return NotFound(result);
    }
}
