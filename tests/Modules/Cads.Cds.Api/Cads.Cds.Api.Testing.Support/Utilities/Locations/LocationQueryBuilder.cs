using Cads.Cds.Api.Testing.Support.Constants;
using System.Web;

namespace Cads.Cds.Api.Testing.Support.Utilities.Locations;

public class LocationQueryBuilder
{
    private string? _cph;
    private DateTime? _lastModifiedDate;

    public LocationQueryBuilder WithCph(string cph)
    {
        _cph = cph;
        return this;
    }

    public LocationQueryBuilder WithLastModifiedDate(DateTime date)
    {
        _lastModifiedDate = date;
        return this;
    }

    public string Build()
    {
        var parameters = new List<string>();

        if (_cph != null)
            parameters.Add($"cph={HttpUtility.UrlEncode(_cph)}");

        if (_lastModifiedDate != null)
            parameters.Add($"lastModifiedDate={HttpUtility.UrlEncode(_lastModifiedDate.Value.ToString("O"))}");

        var qs = string.Join("&", parameters);
        return $"{TestEndpointConstants.ApiLocationRoot}?{qs}";
    }
}