using Cads.Cds.StorageBridge.Testing.Support.BulkLoad.Models;

namespace Cads.Cds.StorageBridge.Testing.Support.BulkLoad.Utilities;

public static class LocationRecordUtilities
{
    public static LocationRecord ParseLocationCsvRow(string csv)
    {
        var parts = csv.Split('|');

        return new LocationRecord(
            RecordType: parts[0],
            RecordCount: int.Parse(parts[1]),
            LocId: int.Parse(parts[2]),
            LocSltId: int.Parse(parts[3]),
            LocLtyId: int.Parse(parts[4]),
            LocCtyId: int.Parse(parts[5]),
            LocReceiveLabelsFlag: parts[6],
            LocEffectiveFrom: DateOnly.Parse(parts[7]),
            LocEffectiveTo: DateOnly.Parse(parts[8]),
            LocCessationReason: parts[9],
            LocPremisesType: parts[10],
            LocComments: parts[11],
            LocMapReference: parts[12],
            LocSourceIdentifier: parts[13],
            LocSourceReference: parts[14],
            LocTelNumber: parts[15],
            LocMobileNumber: parts[16],
            LocFaxNumber: parts[17],
            LocEmailAddress: parts[18],
            LocCurrentStatus: int.Parse(parts[19]),
            LocCurrentUser: parts[20],
            LocCurrentModifiedDate: DateOnly.Parse(parts[21]),
            LocCurrentPid: int.Parse(parts[22]),
            LocReasonCode: parts[23],
            LocVersion: int.Parse(parts[24]),
            LocReceivePpafFlag: parts[25]
        );
    }

    public static LocationRecord MapLocationFromDb(Dictionary<string, object?> row)
    {
        return new LocationRecord(
            RecordType: (string)row["record_type"]!,
            RecordCount: Convert.ToInt32(row["record_count"]),
            LocId: Convert.ToInt32(row["loc_id"]),
            LocSltId: Convert.ToInt32(row["loc_slt_id"]),
            LocLtyId: Convert.ToInt32(row["loc_lty_id"]),
            LocCtyId: Convert.ToInt32(row["loc_cty_id"]),
            LocReceiveLabelsFlag: (string)row["loc_receive_labels_flag"]!,
            LocEffectiveFrom: (DateOnly)row["loc_effective_from"]!,
            LocEffectiveTo: (DateOnly)row["loc_effective_to"]!,
            LocCessationReason: (string)row["loc_cessation_reason"]!,
            LocPremisesType: (string)row["loc_premises_type"]!,
            LocComments: (string)row["loc_comments"]!,
            LocMapReference: (string)row["loc_map_reference"]!,
            LocSourceIdentifier: (string)row["loc_source_identifier"]!,
            LocSourceReference: (string)row["loc_source_reference"]!,
            LocTelNumber: (string)row["loc_tel_number"]!,
            LocMobileNumber: (string)row["loc_mobile_number"]!,
            LocFaxNumber: (string)row["loc_fax_number"]!,
            LocEmailAddress: (string)row["loc_email_address"]!,
            LocCurrentStatus: Convert.ToInt32(row["loc_current_status"]),
            LocCurrentUser: (string)row["loc_current_user"]!,
            LocCurrentModifiedDate: (DateOnly)row["loc_current_modified_date"]!,
            LocCurrentPid: Convert.ToInt32(row["loc_current_pid"]),
            LocReasonCode: (string)row["loc_reason_code"]!,
            LocVersion: Convert.ToInt32(row["loc_version"]),
            LocReceivePpafFlag: (string)row["loc_receive_ppaf_flag"]!
        );
    }
}