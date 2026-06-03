using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Api.Controllers.Requests;

public class GetLocationsRequest
{
    /// <summary>
    /// Filter by a single location CPH identifier.
    /// </summary>
    [FromQuery] public string? Cph { get; set; }

    /// <summary>
    /// Returns only records that have been updated since the provided timestamp (greater than or equal to).
    /// </summary>
    [FromQuery] public DateTime? LastModifiedDate { get; set; }

    /// <summary>
    /// Page number (1-based). Defaults to 1.
    /// </summary>
    [FromQuery] public int Page { get; set; } = 1;

    /// <summary>
    /// Number of records per page. Defaults to 10.
    /// </summary>
    [FromQuery] public int PageSize { get; set; } = 10;

    /// <summary>
    /// The field to order the results by. Available fields for sorting: lid_current_modified_date. Defaults to lid_current_modified_date.
    /// </summary>
    [FromQuery] public string? Order { get; set; }

    /// <summary>
    /// The sort direction. Available values: asc, desc. Defaults to asc.
    /// </summary>
    [FromQuery] public string? Sort { get; set; }
}