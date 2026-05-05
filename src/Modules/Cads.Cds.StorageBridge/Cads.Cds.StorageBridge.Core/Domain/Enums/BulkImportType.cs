using Cads.Cds.StorageBridge.Core.Attributes;

namespace Cads.Cds.StorageBridge.Core.Domain.Enums;

public enum BulkImportType
{
    None,
    [TableName("_ct_animal_relationships")]
    AnimalRelationships,
    [TableName("_ct_animal_statuses")]
    AnimalStatuses,
    [TableName("_ct_condition_markers")]
    ConditionMarkers,
    [TableName("_ct_eartags")]
    EarTags,
    [TableName("_ct_issued_documents")]
    IssuedDocuments,
    [TableName("_ct_parties")]
    Parties,
    [TableName("_ct_registered_animals")]
    RegisteredAnimals,
    [TableName("_ct_registered_movements")]
    RegisteredMovements,
    [TableName("_ct_valid_applications")]
    ValidApplications,
    [TableName("_ct_locations")]
    Locations
}