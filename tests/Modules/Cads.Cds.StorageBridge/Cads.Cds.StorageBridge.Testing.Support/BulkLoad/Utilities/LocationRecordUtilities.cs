using Cads.Cds.StorageBridge.Testing.Support.BulkLoad.Models;
using System.Globalization;

namespace Cads.Cds.StorageBridge.Testing.Support.BulkLoad.Utilities;

public static class LocationRecordUtilities
{
    public static LocationRecord ParseLocationCsvRow(string value, char delimiter = '|')
    {
        var parts = value.Split(delimiter);

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

    // New: map a Dictionary<string, object?> (e.g. LocationsSqlInsertDataDictionary) to LocationRecord.
    public static LocationRecord MapLocationFromDictionary(Dictionary<string, object?> row)
    {
        static string GetString(Dictionary<string, object?> r, string key)
        {
            if (!r.TryGetValue(key, out var v) || v is null) return string.Empty;
            return v is string s ? s : Convert.ToString(v, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        static int GetInt(Dictionary<string, object?> r, string key)
        {
            if (!r.TryGetValue(key, out var v) || v is null) return 0;
            return v switch
            {
                int i => i,
                long l => Convert.ToInt32(l),
                short s => Convert.ToInt32(s),
                decimal d => Convert.ToInt32(d),
                double db => Convert.ToInt32(db),
                string str when int.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out var x) => x,
                _ => Convert.ToInt32(v, CultureInfo.InvariantCulture)
            };
        }

        static DateOnly GetDateOnly(Dictionary<string, object?> r, string key)
        {
            if (!r.TryGetValue(key, out var v) || v is null)
                throw new InvalidOperationException($"Missing date value for '{key}'.");

            return v switch
            {
                DateOnly d => d,
                DateTime dt => DateOnly.FromDateTime(dt),
                string s when DateOnly.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed) => parsed,
                string s when DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDt) => DateOnly.FromDateTime(parsedDt),
                _ => throw new InvalidCastException($"Cannot convert value for '{key}' to DateOnly.")
            };
        }

        return new LocationRecord(
            RecordType: GetString(row, "record_type"),
            RecordCount: GetInt(row, "record_count"),
            LocId: GetInt(row, "loc_id"),
            LocSltId: GetInt(row, "loc_slt_id"),
            LocLtyId: GetInt(row, "loc_lty_id"),
            LocCtyId: GetInt(row, "loc_cty_id"),
            LocReceiveLabelsFlag: GetString(row, "loc_receive_labels_flag"),
            LocEffectiveFrom: GetDateOnly(row, "loc_effective_from"),
            LocEffectiveTo: GetDateOnly(row, "loc_effective_to"),
            LocCessationReason: GetString(row, "loc_cessation_reason"),
            LocPremisesType: GetString(row, "loc_premises_type"),
            LocComments: GetString(row, "loc_comments"),
            LocMapReference: GetString(row, "loc_map_reference"),
            LocSourceIdentifier: GetString(row, "loc_source_identifier"),
            LocSourceReference: GetString(row, "loc_source_reference"),
            LocTelNumber: GetString(row, "loc_tel_number"),
            LocMobileNumber: GetString(row, "loc_mobile_number"),
            LocFaxNumber: GetString(row, "loc_fax_number"),
            LocEmailAddress: GetString(row, "loc_email_address"),
            LocCurrentStatus: GetInt(row, "loc_current_status"),
            LocCurrentUser: GetString(row, "loc_current_user"),
            LocCurrentModifiedDate: GetDateOnly(row, "loc_current_modified_date"),
            LocCurrentPid: GetInt(row, "loc_current_pid"),
            LocReasonCode: GetString(row, "loc_reason_code"),
            LocVersion: GetInt(row, "loc_version"),
            LocReceivePpafFlag: GetString(row, "loc_receive_ppaf_flag")
        );
    }
}