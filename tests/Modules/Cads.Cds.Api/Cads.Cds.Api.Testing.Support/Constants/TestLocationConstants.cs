namespace Cads.Cds.Api.Testing.Support.Constants;

public static class TestLocationConstants
{
    public const string CphIdentifierBase = "10/200/300{0}";

    public static readonly List<string> IntegrationTest_CphIdentifiers =
    [
        "AH-00/477/0081",
        "AH-00/585/0044",
        "AH-00/653/0039",
        "AH-00/723/0102",
        "AH-00/852/6001"
    ];

    public static readonly DateOnly IntegrationTest_LocationModifiedOn = new(2005, 06, 14);
}