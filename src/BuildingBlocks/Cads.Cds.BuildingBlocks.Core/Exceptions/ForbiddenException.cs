namespace Cads.Cds.BuildingBlocks.Core.Exceptions;

/// <summary>
/// Represents a non-retryable authorization failure (e.g. HTTP 403 Forbidden) when calling an
/// external dependency such as S3. Retrying will not help unless the underlying permissions
/// (IAM / resource policy / KMS key policy) are fixed.
/// </summary>
public class ForbiddenException : Exception
{
    public ForbiddenException(string message, Exception inner) : base(message, inner)
    {
    }

    public ForbiddenException(string message) : base(message)
    {
    }
}