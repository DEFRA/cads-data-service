using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;

public class FileAssembler : IFileAssembler
{
    // The sample file format is as follows:
    // H|CTSM_UKV_PROD_BULK_######_CT_ANIMAL_RELATIONSHIPS_2026-02-22-074603.csv|22022026 07:46:03
    // C|RECORD_TYPE|RECORD_COUNT|AAR_ID|AAR_REL_TYPE|AAR_LOC_ID|AAR_CONFIDENCE_INDICATOR|AAR_EFFECTIVE_FROM_DATE|AAR_EFFECTIVE_TO_DATE|AAR_RAN_ID_CHILD|AAR_RAN_ID_PARENT|AAR_PARENT_IDENTIFIER|AAR_PARENT_IDENTIFIER_TYPE|AAR_CANCELLED_REASON|AAR_CURRENT_USER|AAR_CURRENT_STATUS|AAR_CURRENT_MODIFIED_DATE|AAR_CURRENT_PID|AAR_VERSION
    // T | CTSM_UKV_PROD_BULK_######_CT_ANIMAL_RELATIONSHIPS_2026-02-22-074603.csv|22022026 10:16:02|149721673

    private const char Separator ='|';

    private const char HeaderPrefix = 'H';

    private const char ContentPrefix = 'C';

    private const char FooterPrefix = 'T';

    private const string RecordType = "RECORD_TYPE";

    private const string DataPrefix = "D";

    private const string DateTimeFormat = "ddMMyyyy HH:mm:ss";

    public string Create(string fileName, DateTime fileCreatedDateTime, IReadOnlyList<IReadOnlyDictionary<string, object?>> data)
    {
        if (data == null || data.Count == 0)
            throw new ArgumentException("data must contain at least one row", nameof(data));

        // File Header
        // Add the file name and any other necessary header information
        var header = RenderHeader(fileName, data[0], fileCreatedDateTime);

        // File Content
        // Add the data rows
        // Iterate through the data and format it as needed for the file
        var content = RenderContent(data);

        // File Footer
        // Add any necessary footer information
        var footer = RenderFooter(fileName, data.Count, fileCreatedDateTime);

        var fileSb = new StringBuilder();

        // Combine header, content and return
        fileSb.Append(header);
        fileSb.Append(content);
        fileSb.Append(footer);

        return fileSb.ToString();
    }

    private static string RenderHeader(string fileName, IReadOnlyDictionary<string, object?> row, DateTime dateTime)
    {
        var sb = new StringBuilder();
        sb.AppendLine(HeaderPrefix + Separator + fileName + Separator + dateTime.ToString(DateTimeFormat));
        sb.AppendLine(ContentPrefix + Separator + RecordType + Separator + string.Join(Separator, row.Keys.Select(i => i.ToUpper())));

        return sb.ToString().TrimEnd(Separator);
    }

    private static string RenderContent(IReadOnlyList<IReadOnlyDictionary<string, object?>> data)
    {
        var sb = new StringBuilder();

        for (var i = 0; i < data.Count; i++)
        {
            sb.AppendLine(DataPrefix + Separator + $"{i + 1}{Separator}" + string.Join(Separator, data[i].Values));
        }

        return sb.ToString().TrimEnd(Separator);
    }

    private static string RenderFooter(string fileName, long recordCount, DateTime dateTime)
    {
        var sb = new StringBuilder();
        sb.AppendLine(FooterPrefix + Separator + fileName + Separator + dateTime.ToString(DateTimeFormat) + Separator + recordCount);

        return sb.ToString();
    }
}
