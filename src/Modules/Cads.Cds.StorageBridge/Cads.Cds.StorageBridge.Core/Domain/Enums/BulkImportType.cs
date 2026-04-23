using Cads.Cds.StorageBridge.Core.Attributes;

namespace Cads.Cds.StorageBridge.Core.Domain.Enums;

public enum BulkImportType
{
    None,
    [TableName("ct_animal_relationships")]
    AnimalRelationships,
    [TableName("ct_animal_statuses")]
    AnimalStatuses,
    [TableName("ct_condition_markers")]
    ConditionMarkers,
    [TableName("ct_eartags")]
    EarTags,
    [TableName("ct_issued_documents")]
    IssuedDocuments,
    [TableName("ct_parties")]
    Parties,
    [TableName("ct_registered_animals")]
    RegisteredAnimals,
    [TableName("ct_registered_movements")]
    RegisteredMovements,
    [TableName("ct_valid_applications")]
    ValidApplications
}