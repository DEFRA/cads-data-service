namespace Cads.Cds.SystemAdmin.Application.Generation.Utils;

public interface IFileAssembler
{
    string Create(string fileName, DateTime fileCreatedDateTime, IReadOnlyList<IReadOnlyDictionary<string, object?>> data);
}
