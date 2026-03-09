using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Holdings;

public class GetHoldingsQuery : PagedQuery<UkvDto>
{
    public DateTime? LastModified { get; set; }
}