using Cads.Cds.StorageBridge.Core.Attributes;

namespace Cads.Cds.StorageBridge.Core.Domain.Enums;

public enum BulkLoadDataType
{
    None,
    [TableName("ct_animal_relationships", "aar_id", "cts")]
    AnimalRelationships,
    [TableName("ct_animal_statuses", "ast_id", "cts")]
    AnimalStatuses,
    [TableName("ct_condition_markers", "com_id", "cts")]
    ConditionMarkers,
    [TableName("ct_eartags", "etg_id", "cts")]
    EarTags,
    [TableName("ct_issued_documents", "ido_id", "cts")]
    IssuedDocuments,
    [TableName("ct_parties", "par_id", "cts")]
    Parties,
    [TableName("ct_registered_animals", "ran_id", "cts")]
    RegisteredAnimals,
    [TableName("ct_registered_movements", "mov_id", "cts")]
    RegisteredMovements,
    [TableName("ct_valid_applications", "vap_id", "cts")]
    ValidApplications,
    [TableName("ct_locations", "loc_id", "cts")]
    Locations
}