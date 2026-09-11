using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Logging;
using Cads.Cds.StorageBridge.Infrastructure.Messaging.Consumers;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.S3Import.Services;

public class DefensiveCopyLineNormaliserTests
{
    private const char Delimiter = '|';
    private const int CtParamValueColumnCount = 14;
    private const int CtSuspenseWgAllocRulesColumnCount = 13;
    private const int CtMovtCorrectSummariesColumnCount = 38;
    private readonly Mock<ILogger<DefensiveCopyLineNormaliser>> _loggerMock =
        new Mock<ILogger<DefensiveCopyLineNormaliser>>().EnableAllLogLevels();
    private readonly DefensiveCopyLineNormaliser _sut;

    public DefensiveCopyLineNormaliserTests()
    {
        _sut = new DefensiveCopyLineNormaliser(_loggerMock.Object);
    }

    [Theory]
    [InlineData(
        "D |292|2019|CP.INF_OLMPRI|410|5|3|Normal Off|5|m165564|1|10-AUG-01||1",
        "D |292|2019|CP.INF_OLMPRI|410|5|3|Normal Off|5|m165564|1|10-AUG-01||1")]
    [InlineData(
        "D |292|2019|CP.INF_OLMPRI|410|SEO|CHR~GAP~BDR|3|SEO GAP BDR|SEO GAP BDR OVERRIDE|5|m165564|1|10-AUG-01||1",
        "D |292|2019|CP.INF_OLMPRI|410|\"SEO|CHR~GAP~BDR\"|3|\"SEO GAP BDR|SEO GAP BDR OVERRIDE\"|5|m165564|1|10-AUG-01||1")]
    [InlineData(
        "D|2474|20788|CP.LIP_DELTA_JAVA_COMMAND|20401|0||/usr/java7_64/jre/bin/java|-jar|/ctsm/app02/ctsal/external/PRCG/CTS_OWN/BIN/encryptionUtil.jar|-e|-i||x912716|1|17-SEP-20||1",
        "D|2474|20788|CP.LIP_DELTA_JAVA_COMMAND|20401|0||\"/usr/java7_64/jre/bin/java|-jar|/ctsm/app02/ctsal/external/PRCG/CTS_OWN/BIN/encryptionUtil.jar|-e|-i\"||x912716|1|17-SEP-20||1")]
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

    [Theory]
    [InlineData(
        "D|1500112|2911314|f800702|S|23-NOV-00|42|||41457581||SMC|13-NOV-00|MHF2000318SMC00411|153|MHF2000318SMC00411|153|UK C4491 00206||||||2000-11-13|2000000|23184041657||||Validation Error|N|Determined Movement Type| Location & Move|DLOC|m174006||Submitted|N|1",
        "D|1500112|2911314|f800702|S|23-NOV-00|42|||41457581||SMC|13-NOV-00|MHF2000318SMC00411|153|MHF2000318SMC00411|153|UK C4491 00206||||||2000-11-13|2000000|23184041657||||Validation Error|N|\"Determined Movement Type| Location & Move\"|DLOC|m174006||Submitted|N|1")]
    [InlineData(
        "D|1507093|4091028|f800702|D|17-SEP-01|42|||55514366||OLM|17-SEP-01|||||UK343559700109|SH|5269||7|2001-09-06|2001-09-17|2000025|36|50||||On-line Entry|N|Delete Duplicate Movement||x907151||Deleted|N|1",
        "D|1507093|4091028|f800702|D|17-SEP-01|42|||55514366||OLM|17-SEP-01|||||UK343559700109|SH|5269||7|2001-09-06|2001-09-17|2000025|36|50|||On-line Entry|N|Delete Duplicate Movement||x907151||Deleted|N|1")]
    [InlineData(
        "D|1541757|7499234|f800702|S|04-NOV-02|42|||59640051||SMC|23-NOV-01|MHF2001326SMC00387|157|MHF2001326SMC00387|157|UK W4372 00251|AH|08/395/0043||||2001-11-22|2000000|33266037077||||Validation Error|N|Determined Movement Type| Location & Move|DM3|m174967||Submitted|N|1",
        "D|1541757|7499234|f800702|S|04-NOV-02|42|||59640051||SMC|23-NOV-01|MHF2001326SMC00387|157|MHF2001326SMC00387|157|UK W4372 00251|AH|08/395/0043||||2001-11-22|2000000|33266037077||||Validation Error|N|\"Determined Movement Type| Location & Move\"|DM3|m174967||Submitted|N|1")]
    [InlineData(
        "D|1584651|2912105|f800702|S|23-NOV-00|42|||41482920||SMC|13-NOV-00|MHF2000318SMC00679|25|MHF2000318SMC00679|25|UK A1736 00371||||||2000-11-13|2000000|23181069808||||Validation Error|N|Determined Movement Type| Location & Move|DLOC|m168551||Submitted|N|1",
        "D|1584651|2912105|f800702|S|23-NOV-00|42|||41482920||SMC|13-NOV-00|MHF2000318SMC00679|25|MHF2000318SMC00679|25|UK A1736 00371||||||2000-11-13|2000000|23181069808||||Validation Error|N|\"Determined Movement Type| Location & Move\"|DLOC|m168551||Submitted|N|1")]
    public void Normalise_WhentMovtCorrectSummariesRules_ShouldReturnExpectedLine(string input, string expected)
    {
        var result = _sut.Normalise(input, ImportDataType.CtMovtCorrectSummaries, Delimiter, CtMovtCorrectSummariesColumnCount);

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