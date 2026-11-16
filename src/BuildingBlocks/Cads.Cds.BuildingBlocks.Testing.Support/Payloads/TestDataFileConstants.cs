using Amazon.S3.Model;
using System.Globalization;
using System.Text;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Payloads;

public static class TestDataFileConstants
{
    public const string AnimalInsertSqlStatement = "INSERT INTO animals VALUES (1, 'Cow');";

    public static string LocationsHeader =>
        "record_type|record_count|loc_id|loc_slt_id|loc_lty_id|loc_cty_id|loc_receive_labels_flag|loc_effective_from|loc_effective_to|loc_cessation_reason|loc_premises_type|loc_comments|loc_map_reference|loc_source_identifier|loc_source_reference|loc_tel_number|loc_mobile_number|loc_fax_number|loc_email_address|loc_current_status|loc_current_user|loc_current_modified_date|loc_current_pid|loc_reason_code|loc_version|loc_receive_ppaf_flag";

    public static string LocationsDataRow1 =>
        "D|1|101|1|2|33|N|01-JUL-96|17-JAN-98|BC|AH|Row 1 comments|TL 123456|VT|12345678|0201234567|07712345678|0209876543|email1@internal.test|1|m100000|14-JUN-05|29|AC|1|Y";

    public static string LocationsDataRow2 =>
        "D|2|102|1|2|88|N|01-JUL-21|17-JAN-23|BC|AH|Row 2 comments|TL 234567|VT|23456789|0202345678|07723456789|0209876543|email2@internal.test|1|m100000|10-JUN-25|29|AC|1|Y";

    public static string LocationsDataRow3 =>
        "D|3|103|1|2|88|N|01-JUL-21|17-JAN-23|BC|AH|Row 3 comments|TL 345678|VT|34567890|0203456789|07734567890|0209876543|email3@internal.test|1|m100000|10-JUN-25|29|AC|1|Y";

    public static string LocationsDataRow4 =>
        "D|4|104|1|2|88|N|01-JUL-21|17-JAN-23|BC|AH|Row 4 comments|TL 456789|VT|45678901|0204567890|07745678901|0209876543|email4@internal.test|1|m100000|10-JUN-25|29|AC|1|Y";

    public static string LocationsDataRow5 =>
        "D|5|105|1|2|88|N|01-JUL-21|17-JAN-23|BC|AH|Row 5 comments|TL 567890|VT|56789012|0205678901|07756789012|0209876543|email5@internal.test|1|m100000|10-JUN-25|29|AC|1|Y";

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

    public static string InvalidLocationsSqlInsertStatement => "INSERT INTO cts.ct_locations (";

    public static GetObjectResponse FakeFileContent(string content) => new()
    {
        ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(content))
    };

    public static string DictionaryToSqlInsert(Dictionary<string, object?> data, string tableName = "cts.ct_locations")
    {
        if (data is null || data.Count == 0)
        {
            throw new ArgumentException("Data dictionary must contain at least one column.", nameof(data));
        }

        var columns = string.Join(", ", data.Keys);
        var values = string.Join(", ", data.Values.Select(FormatSqlValue));

        return $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
    }

    private static string FormatSqlValue(object? value)
    {
        if (value is null)
        {
            return "NULL";
        }

        if (value is DateTime dateTime)
        {
            return dateTime.TimeOfDay == TimeSpan.Zero
                ? $"'{dateTime:yyyy-MM-dd}'"
                : $"'{dateTime:yyyy-MM-dd HH:mm:ss.ffffff}'";
        }

        if (value is DateOnly dateOnly)
        {
            return $"'{dateOnly:yyyy-MM-dd}'";
        }

        if (value is string text)
        {
            return $"'{text.Replace("'", "''")}'";
        }

        if (value is char character)
        {
            return $"'{character}'";
        }

        if (value is bool boolean)
        {
            return boolean ? "1" : "0";
        }

        if (value is IFormattable formattable)
        {
            return formattable.ToString(null, CultureInfo.InvariantCulture) ?? "NULL";
        }

        return $"'{value.ToString()?.Replace("'", "''")}'";
    }
}