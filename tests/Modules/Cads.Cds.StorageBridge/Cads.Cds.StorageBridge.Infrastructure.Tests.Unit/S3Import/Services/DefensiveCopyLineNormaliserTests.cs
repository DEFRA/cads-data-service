using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;
using FluentAssertions;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.S3Import.Services;

public class DefensiveCopyLineNormaliserTests
{
    private const char Delimiter = '|';
    private const int CtParamValueColumnCount = 14;
    private const int CtSuspenseWgAllocRulesColumnCount = 13;

    private readonly DefensiveCopyLineNormaliser _sut = new();

    [Theory]
    [InlineData(
        "D |292|2019|CP.INF_OLMPRI|410|5|3|Normal Off|5|m165564|1|10-AUG-01||1",
        "D |292|2019|CP.INF_OLMPRI|410|5|3|Normal Off|5|m165564|1|10-AUG-01||1")]
    [InlineData(
        "D |292|2019|CP.INF_OLMPRI|410|SEO|CHR~GAP~BDR|3|Normal Off|5|m165564|1|10-AUG-01||1",
        "D |292|2019|CP.INF_OLMPRI|410|\"SEO|CHR~GAP~BDR\"|3|Normal Off|5|m165564|1|10-AUG-01||1")]
    [InlineData(
        "D |292|2019|CP.INF_OLMPRI|410|5|3|SEO GAP BDR|SEO GAP BDR OVERRIDE|5|m165564|1|10-AUG-01||1",
        "D |292|2019|CP.INF_OLMPRI|410|5|3|\"SEO GAP BDR|SEO GAP BDR OVERRIDE\"|5|m165564|1|10-AUG-01||1")]
    [InlineData(
        "D |292|2019|CP.INF_OLMPRI|410|SEO|CHR~GAP~BDR|3|SEO GAP BDR|SEO GAP BDR OVERRIDE|5|m165564|1|10-AUG-01||1",
        "D |292|2019|CP.INF_OLMPRI|410|\"SEO|CHR~GAP~BDR\"|3|\"SEO GAP BDR|SEO GAP BDR OVERRIDE\"|5|m165564|1|10-AUG-01||1")]
    public void Normalise_WhenCtParamValue_ShouldReturnExpectedLine(string input, string expected)
    {
        var result = _sut.Normalise(input, ImportDataType.CtParamValue, Delimiter, CtParamValueColumnCount);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(
        "D|1 |180|5|60|NPP\tPMX\t&\tRPL\t&\t^=|| |f800702|1|26-SEP-03|99|1",
        "D|1 |180|5|60|NPP\tPMX\t&\tRPL\t&\t^=|| |f800702|1|26-SEP-03|99|1")]
    [InlineData(
        "D|1 |180|5|60|PMX\t!\tAS_ATYP\tO\t=\t0\tAS_VGDTA\t>\t0\tAS_VSIET\t>\t|\tAS_SRCTP\tPPM\t=\tAS_SRCTP\tOLD\t=\t|\t&\t&\t&\t^=|| |f800702|1|26-SEP-03|99|1",
        "D|1 |180|5|60|\"PMX\t!\tAS_ATYP\tO\t=\t0\tAS_VGDTA\t>\t0\tAS_VSIET\t>\t|\tAS_SRCTP\tPPM\t=\tAS_SRCTP\tOLD\t=\t|\t&\t&\t&\t^=\"|| |f800702|1|26-SEP-03|99|1")]
    [InlineData(
        "D|1 |180|5|60|PMX\t!\tAS_ATYP\tO\t=\t0\tAS_VGDTA\t>\t0\tAS_VSIET\t>\t|\tAS_SRCTP\tPPM\t=\tAS_SRCTP\tOLD\t=\t&\t&\t&\t^=|| |f800702|1|26-SEP-03|99|1",
        "D|1 |180|5|60|\"PMX\t!\tAS_ATYP\tO\t=\t0\tAS_VGDTA\t>\t0\tAS_VSIET\t>\t|\tAS_SRCTP\tPPM\t=\tAS_SRCTP\tOLD\t=\t&\t&\t&\t^=\"|| |f800702|1|26-SEP-03|99|1")]
    public void Normalise_WhenCtSuspenseWgAllocRules_ShouldReturnExpectedLine(string input, string expected)
    {
        var result = _sut.Normalise(input, ImportDataType.CtSuspenseWgAllocRules, Delimiter, CtSuspenseWgAllocRulesColumnCount);

        result.Should().Be(expected);
    }

    [Fact]
    public void Normalise_WhenImportTypeIsUnsupported_ShouldReturnOriginalLine()
    {
        const string input = "A|B|C|D|E";

        var result = _sut.Normalise(input, ImportDataType.CtLocations, Delimiter, CtParamValueColumnCount);

        result.Should().Be(input);
    }
}