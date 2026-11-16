namespace Cads.Cds.SystemAdmin.Application.Generation.Utils;

/// <summary>Formats values the way the vendor extract does.</summary>
public interface IEnvelopeValueFormatter
{
    string FormatValue(object? value);

    string FormatAudDateTime(DateTime value);

    string FormatEnvelopeDateTime(DateTime value);
}