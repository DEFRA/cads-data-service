namespace Cads.Cds.BuildingBlocks.Application.Imports.Utilities;

public interface ICtsmFilenameGenerator
{
    string Generate(CtsmFilenameRequest request);
}
