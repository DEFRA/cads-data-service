namespace Cads.Cds.SystemAdmin.Application.Generation.Utils;

public interface IFileNameGenerator
{
    string Create(CreateFileNameCommand command);
}