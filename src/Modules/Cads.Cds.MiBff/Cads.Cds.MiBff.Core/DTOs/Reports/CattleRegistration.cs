namespace Cads.Cds.MiBff.Core.DTOs.Reports;

public class CattleRegistration
{
    public string BirthYear { get; set; } = "2024";
    public string BirthMonth { get; set; } = "January";
    public string Country { get; set; } = "England";
    public string GovRegion { get; set; } = "South East";
    public string County { get; set; } = "Oxfordshire";
    public string BreedType { get; set; } = "NonDairy";
    public string Breed { get; set; } = "Aberdeen Angus";
    public string Sex { get; set; } = "F";
    public string ApplicationType { get; set; } = "Birth Application";
    public string NumberOfBirths { get; set; } = "19";

    public static List<CattleRegistration> GetFakeData(int rows)
    {
        Random rnd = new Random();
        return Enumerable.Range(1, 20).Select(x => new CattleRegistration() { NumberOfBirths = rnd.Next(1, 25).ToString()}).ToList();
    }
}