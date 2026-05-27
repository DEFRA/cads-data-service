namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Streams;

public class NonDisposingStreamWriter(Stream stream) : StreamWriter(stream)
{
    protected override void Dispose(bool disposing)
    {
        base.Flush();
    }
}