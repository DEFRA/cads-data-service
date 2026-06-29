using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Core.Attributes;

namespace Cads.Cds.StorageBridge.Core.Domain.Enums;

public enum ImportDataType
{
    None,
    [TableInfo("ct_animal_relationships", "aar_id", SchemaName.Cts)]
    [TableInfo("ct_animal_relationships", "aar_id", SchemaName.CtsTransactions)]
    AnimalRelationships,
    [TableInfo("ct_animal_statuses", "ast_id", SchemaName.Cts)]
    [TableInfo("ct_animal_statuses", "ast_id", SchemaName.CtsTransactions)]
    AnimalStatuses,
    [TableInfo("ct_condition_markers", "com_id", SchemaName.Cts)]
    [TableInfo("ct_condition_markers", "com_id", SchemaName.CtsTransactions)]
    ConditionMarkers,
    [TableInfo("ct_eartags", "etg_id", SchemaName.Cts)]
    [TableInfo("ct_eartags", "etg_id", SchemaName.CtsTransactions)]
    EarTags,
    [TableInfo("ct_issued_documents", "ido_id", SchemaName.Cts)]
    [TableInfo("ct_issued_documents", "ido_id", SchemaName.CtsTransactions)]
    IssuedDocuments,
    [TableInfo("ct_parties", "par_id", SchemaName.Cts)]
    [TableInfo("ct_parties", "par_id", SchemaName.CtsTransactions)]
    Parties,
    [TableInfo("ct_registered_animals", "ran_id", SchemaName.Cts)]
    [TableInfo("ct_registered_animals", "ran_id", SchemaName.CtsTransactions)]
    RegisteredAnimals,
    [TableInfo("ct_registered_movements", "mov_id", SchemaName.Cts)]
    [TableInfo("ct_registered_movements", "mov_id", SchemaName.CtsTransactions)]
    RegisteredMovements,
    [TableInfo("ct_valid_applications", "vap_id", SchemaName.Cts)]
    [TableInfo("ct_valid_applications", "vap_id", SchemaName.CtsTransactions)]
    ValidApplications,
    [TableInfo("ct_locations", "loc_id", SchemaName.Cts)]
    [TableInfo("ct_locations", "loc_id", SchemaName.CtsTransactions)]
    Locations
}