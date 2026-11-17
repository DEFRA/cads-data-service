using System.Text;

namespace Cads.Cds.SystemAdmin.Application.Generation.Utils;

public interface IEnvelopeWriter
{
    void WriteRecord(StringBuilder sb, IEnumerable<string> fields);
}
