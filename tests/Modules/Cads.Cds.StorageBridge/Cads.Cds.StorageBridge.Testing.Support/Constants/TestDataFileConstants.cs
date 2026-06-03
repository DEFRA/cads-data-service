using Amazon.S3.Model;
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

    public static string LocationsSqlInsertLocationsDataRow1 =>
       "INSERT INTO _ct_locations (loc_receive_ppaf_flag, loc_id, loc_slt_id, loc_lty_id, loc_cty_id, loc_receive_labels_flag, loc_effective_from, loc_effective_to, loc_cessation_reason, loc_premises_type, loc_comments, loc_map_reference, loc_source_identifier, loc_source_reference, loc_tel_number, loc_mobile_number, loc_fax_number, loc_email_address, loc_current_status, loc_current_user, loc_current_modified_date, loc_current_pid, loc_reason_code, loc_version, row_number, record_type, record_count, imported_date) " +
       "VALUES ('Y', 1, NULL, 2, 278, 'N', DATE '1996-07-01', DATE '1998-01-17', 'BC', 'AH', 'PA-COMMENT-1', NULL, 'VT', '99037438', 'PA-TEL-1', 'PA-MOB-1', 'PA-FAX-1', 'pa_email_1@defra.gov.uk', '1', 'm184910', DATE '2005-06-14', 29, 'AC', 1, 1, 'D', 1, TIMESTAMP '2026-05-01 17:05:45.95947')";

    public static string LocationsSqlInsertLocationsDataRow2 =>
       "INSERT INTO _ct_locations (loc_receive_ppaf_flag, loc_id, loc_slt_id, loc_lty_id, loc_cty_id, loc_receive_labels_flag, loc_effective_from, loc_effective_to, loc_cessation_reason, loc_premises_type, loc_comments, loc_map_reference, loc_source_identifier, loc_source_reference, loc_tel_number, loc_mobile_number, loc_fax_number, loc_email_address, loc_current_status, loc_current_user, loc_current_modified_date, loc_current_pid, loc_reason_code, loc_version, row_number, record_type, record_count, imported_date) " +
       "('Y', 2, NULL, 2, 278, 'N', DATE '1996-07-01', DATE '1998-01-17', 'BC', 'AH', 'PA-COMMENT-2', NULL, 'VT', '99012657', 'PA-TEL-2', 'PA-MOB-2', 'PA-FAX-2', 'pa_email_2@defra.gov.uk', '1', 'm184910', DATE '2005-06-14', 29, 'AC', 1, 2, 'D', 2, TIMESTAMP '2026-05-01 17:05:45.95947'),\r\n    ";

    public static string InvalidSqlInsertLocationsDataRow1 =>
       "INSERT INTO _ct_locations (";

    public static GetObjectResponse FakeFileContent(string content)
    {
        return new GetObjectResponse
        {
            ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(content))
        };
    }
}