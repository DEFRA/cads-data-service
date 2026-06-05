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
    [FromQuery] public DateOnly? LastModifiedDate { get; set; }
}