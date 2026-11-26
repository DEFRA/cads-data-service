namespace Cads.Cds.Api.Testing.Support.Constants;

public static class TestLocationConstants
{
    public const string CphIdentifierBase = "10/200/300{0}";

    public static readonly List<string> IntegrationTest_CphIdentifiers =
    [
        "AH-00/101/1001",
        "AH-00/102/1002",
        "AH-00/103/1003",
        "AH-00/104/1004",
        "AH-00/105/1005"
    ];

    public static readonly DateOnly IntegrationTest_LocationModifiedOn = new(2005, 06, 14);
}