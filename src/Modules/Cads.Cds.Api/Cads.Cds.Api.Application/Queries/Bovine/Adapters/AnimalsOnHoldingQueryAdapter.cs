using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Application.DTOs.Holdings;
using Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;

namespace Cads.Cds.Api.Application.Queries.Bovine.Adapters;

public class AnimalsOnHoldingQueryAdapter(IAnimalsOnHoldingReadQuery readQuery)
{
    private const string DefaultResourceType = "AnimalCollection";
    private const string DefaultCphSchema = "uk.gov.defra.cph";

    public async Task<AnimalsOnHoldingDto?> SearchAsync(
        GetAnimalsOnHolding query,
        CancellationToken cancellationToken = default)
    {
        var data = await readQuery.ExecuteAsync(query, cancellationToken);

        // With no rows (empty filter result or page past the end) fall back to the request.
        var first = data.Items.Count > 0 ? data.Items[0] : null;

        return new AnimalsOnHoldingDto
        {
            ResourceType = first?.ResourceType ?? DefaultResourceType,
            Cph = new HoldingIdentifierDto
            {
                Schema = first?.CphSchema ?? DefaultCphSchema,
                Identifier = first?.CphNumber ?? query.Cph
            },
            LocationName = first?.LocationName,
            Animals = data.Items.ToDtoList(),
            TotalRecords = data.TotalCount,
            Page = data.Page,
            PageSize = data.PageSize
        };
    }
}