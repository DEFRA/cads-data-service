namespace Cads.Cds.Api.Testing.Support.Constants;

public static class TestBovineConstants
{
    public const string KnownCph = "12/345/6789";
    public const string OtherCph = "98/765/4321";

    public const string KnownIdentifier = "UK324537113234";
    public const string UnknownIdentifier = "UK999999999999";

    // Seeded by AnimalOnHoldingDataFactory (known CPH)
    public const int KnownCphAnimalCount = 30;       // even index = Female (15), index % 3 == 0 = HO (10)
    public const int KnownCphFemaleHoCount = 5;      // index % 6 == 0
    public const int OtherCphAnimalCount = 5;
}