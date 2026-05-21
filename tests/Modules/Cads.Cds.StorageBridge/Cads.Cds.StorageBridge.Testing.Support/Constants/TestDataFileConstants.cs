using Amazon.S3.Model;
using System.Text;

namespace Cads.Cds.StorageBridge.Testing.Support.Constants;

public static class TestDataFileConstants
{
    /*
        record_type|
        record_count|
        loc_id|
        loc_slt_id| XXXX
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
        "D|1|1|XXXX|2|278|N|01-JUL-96|17-JAN-98|BC|AH|Row 1 comments|TL 123456|VT|12345678|0201234567|07712345678|0209876543|email1@internal.test|1|m100000|14-JUN-05|29|AC|1|Y";

    public static string LocationsDataRow2 =>
        "D|2|2|XXXX|2|278|N|01-JUL-21|17-JAN-23|BC|AH|Row 2 comments|TL 234567|VT|23456789|0202345678|07723456789|0209876543|email2@internal.test|1|m100000|10-JUN-25|29|AC|1|Y";

    public static GetObjectResponse FakeCsvFileContent(string content)
    {
        return new GetObjectResponse
        {
            ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(content))
        };
    }
}
