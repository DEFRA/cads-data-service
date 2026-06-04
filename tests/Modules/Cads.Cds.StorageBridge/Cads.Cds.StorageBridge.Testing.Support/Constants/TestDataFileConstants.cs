using Amazon.S3.Model;
using System.Globalization;
using System.Text;

namespace Cads.Cds.StorageBridge.Testing.Support.Constants;

public static class TestDataFileConstants
{
    /*
        record_type|
        record_count|
        loc_id|
        loc_slt_id|
        loc_lty_id| 
        loc_cty_id|
        loc_receive_labels_flag|
        loc_effective_from|
        loc_effective_to|
        loc_cessation_reason|
        loc_premises_type|
        loc_comments|
        loc_map_reference|
        loc_source_identifier|
        loc_source_reference|
        loc_tel_number|
        loc_mobile_number|
        loc_fax_number|
        loc_email_address|
        loc_current_status|
        loc_current_user|
        loc_current_modified_date|
        loc_current_pid|
        loc_reason_code|
        loc_version|
        loc_receive_ppaf_flag
    */
    public static string LocationsHeader =>
        "record_type|record_count|loc_id|loc_slt_id|loc_lty_id|loc_cty_id|loc_receive_labels_flag|loc_effective_from|loc_effective_to|loc_cessation_reason|loc_premises_type|loc_comments|loc_map_reference|loc_source_identifier|loc_source_reference|loc_tel_number|loc_mobile_number|loc_fax_number|loc_email_address|loc_current_status|loc_current_user|loc_current_modified_date|loc_current_pid|loc_reason_code|loc_version|loc_receive_ppaf_flag";

    public static string LocationsDataRow1 =>
        "D|1|1|1|2|33|N|01-JUL-96|17-JAN-98|BC|AH|Row 1 comments|TL 123456|VT|12345678|0201234567|07712345678|0209876543|email1@internal.test|1|m100000|14-JUN-05|29|AC|1|Y";

    public static string LocationsDataRow2 =>
        "D|2|2|1|2|88|N|01-JUL-21|17-JAN-23|BC|AH|Row 2 comments|TL 234567|VT|23456789|0202345678|07723456789|0209876543|email2@internal.test|1|m100000|10-JUN-25|29|AC|1|Y";

    public static string InvalidLocationsDataRow1 =>
        "D|1|1|1|ABC|DEF|GHI|2|33|N|XX|true|N";

    public static Dictionary<string, object?> LocationsSqlInsertDataDictionary => new()
    {
        ["loc_receive_ppaf_flag"] = "Y",
        ["loc_id"] = 99999,
        ["loc_slt_id"] = null,
        ["loc_lty_id"] = 2,
        ["loc_cty_id"] = 278,
        ["loc_receive_labels_flag"] = "N",
        ["loc_effective_from"] = new DateOnly(1996, 7, 1),
        ["loc_effective_to"] = new DateOnly(1998, 1, 17),
        ["loc_cessation_reason"] = "BC",
        ["loc_premises_type"] = "AH",
        ["loc_comments"] = "PA-COMMENT-1",
        ["loc_map_reference"] = null,
        ["loc_source_identifier"] = "VT",
        ["loc_source_reference"] = "99037438",
        ["loc_tel_number"] = "PA-TEL-1",
        ["loc_mobile_number"] = "PA-MOB-1",
        ["loc_fax_number"] = "PA-FAX-1",
        ["loc_email_address"] = "pa_email_1@defra.gov.uk",
        ["loc_current_status"] = "1",
        ["loc_current_user"] = "m184910",
        ["loc_current_modified_date"] = new DateOnly(2005, 6, 14),
        ["loc_current_pid"] = 29,
        ["loc_reason_code"] = "AC",
        ["loc_version"] = 1,
        ["row_number"] = 1,
        ["record_type"] = "D",
        ["record_count"] = 1,
        ["imported_date"] = DateTime.Parse("2026-05-01 17:05:45.95947")
    };

    public static string LocationsSqlInsertStatement => DictionaryToSqlInsert(LocationsSqlInsertDataDictionary);

    public static string InvalidLocationsSqlInsertStatement =>
       "INSERT INTO _ct_locations (";

    public static GetObjectResponse FakeFileContent(string content)
    {
        return new GetObjectResponse
        {
            ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(content))
        };
    }

    public static string DictionaryToSqlInsert(Dictionary<string, object?> data, string tableName = "_ct_locations")
    {
        if (data == null || data.Count == 0)
            throw new ArgumentException("Data dictionary must contain at least one column.", nameof(data));

        var columns = string.Join(", ", data.Keys);
        var values = string.Join(", ", data.Values.Select(FormatSqlValue));

        return $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
    }

    private static string FormatSqlValue(object? value)
    {
        if (value is null)
            return "NULL";

        if (value is DateTime dt)
        {
            // If time component is zero, use DATE, otherwise TIMESTAMP
            if (dt.TimeOfDay == TimeSpan.Zero)
                return $"'{dt:yyyy-MM-dd}'";
            return $"'{dt:yyyy-MM-dd HH:mm:ss.ffffff}'";
        }
        if (value is DateOnly dateonly)
        {
            return $"'{dateonly:yyyy-MM-dd}'";
        }

        if (value is string s)
            return $"'{s.Replace("'", "''")}'";

        if (value is char c)
            return $"'{c}'";

        if (value is bool b)
            return b ? "1" : "0";

        // Numeric / formattable types
        if (value is IFormattable formattable)
            return formattable.ToString(null, CultureInfo.InvariantCulture) ?? "NULL";

        // Fallback to quoted string
        return $"'{value.ToString()?.Replace("'", "''")}'";
    }
}