using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Core.Attributes;

namespace Cads.Cds.StorageBridge.Core.Domain.Enums;

public enum BulkLoadDataType
{
    None,
    [TableName("ct_animal_relationships", "aar_id", SchemaName.Cts)]
    AnimalRelationships,
    [TableName("ct_animal_statuses", "ast_id", SchemaName.Cts)]
    AnimalStatuses,
    [TableName("ct_condition_markers", "com_id", SchemaName.Cts)]
    ConditionMarkers,
    [TableName("ct_eartags", "etg_id", SchemaName.Cts)]
    EarTags,
    [TableName("ct_issued_documents", "ido_id", SchemaName.Cts)]
    IssuedDocuments,
    [TableName("ct_parties", "par_id", SchemaName.Cts)]
    Parties,
    [TableName("ct_registered_animals", "ran_id", SchemaName.Cts)]
    RegisteredAnimals,
    [TableName("ct_registered_movements", "mov_id", SchemaName.Cts)]
    RegisteredMovements,
    [TableName("ct_valid_applications", "vap_id", SchemaName.Cts)]
    ValidApplications,
    [TableName("ct_locations", "loc_id", SchemaName.Cts)]
    Locations
}