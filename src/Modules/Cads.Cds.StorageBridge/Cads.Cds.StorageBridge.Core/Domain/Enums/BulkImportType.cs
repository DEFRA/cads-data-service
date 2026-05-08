using Cads.Cds.StorageBridge.Core.Attributes;

namespace Cads.Cds.StorageBridge.Core.Domain.Enums;

public enum BulkImportType
{
    None,
    [TableName("_ct_animal_relationships", "aar_id")]
    AnimalRelationships,
    [TableName("_ct_animal_statuses", "ast_id")]
    AnimalStatuses,
    [TableName("_ct_condition_markers", "com_id")]
    ConditionMarkers,
    [TableName("_ct_eartags", "etg_id")]
    EarTags,
    [TableName("_ct_issued_documents","ido_id")]
    IssuedDocuments,
    [TableName("_ct_parties", "par_id")]
    Parties,
    [TableName("_ct_registered_animals","ran_id")]
    RegisteredAnimals,
    [TableName("_ct_registered_movements", "mov_id")]
    RegisteredMovements,
    [TableName("_ct_valid_applications", "vap_id")]
    ValidApplications,
    [TableName("_ct_locations", "loc_id")]
    Locations
}