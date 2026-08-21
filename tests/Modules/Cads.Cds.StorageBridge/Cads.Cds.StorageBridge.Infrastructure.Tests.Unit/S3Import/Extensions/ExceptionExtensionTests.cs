using Cads.Cds.StorageBridge.Infrastructure.S3Import.Extensions;
using FluentAssertions;
using Npgsql;
using System.Data.Common;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.S3Import.Extensions;

public class ExceptionExtensionTests
{
    [Theory]
    [InlineData("08000")] // connection_exception
    [InlineData("08006")] // connection_failure
    [InlineData("40001")] // serialization_failure
    [InlineData("40P01")] // deadlock_detected
    [InlineData("53300")] // too_many_connections
    [InlineData("57P03")] // cannot_connect_now
    [InlineData("58030")] // io_error
    public void IsTransientPostgresException_WhenPostgresExceptionHasTransientSqlState_ShouldReturnTrue(string sqlState)
    {
        var ex = new PostgresException("boom", "ERROR", "ERROR", sqlState);

        ex.IsTransientPostgresException().Should().BeTrue();
    }

    [Theory]
    [InlineData("23505")] // unique_violation
    [InlineData("23503")] // foreign_key_violation
    [InlineData("22P02")] // invalid_text_representation
    [InlineData("42601")] // syntax_error
    [InlineData("42501")] // insufficient_privilege
    [InlineData("00000")] // successful_completion
    public void IsTransientPostgresException_WhenPostgresExceptionHasNonTransientSqlState_ShouldReturnFalse(string sqlState)
    {
        var ex = new PostgresException("boom", "ERROR", "ERROR", sqlState);

        ex.IsTransientPostgresException().Should().BeFalse();
    }

    [Theory]
    [InlineData("")]   // empty
    [InlineData("0")]  // shorter than the two-char class prefix
    public void IsTransientPostgresException_WhenPostgresExceptionHasMissingOrShortSqlState_ShouldReturnFalse(string sqlState)
    {
        var ex = new PostgresException("boom", "ERROR", "ERROR", sqlState);

        ex.IsTransientPostgresException().Should().BeFalse();
    }

    [Fact]
    public void IsTransientPostgresException_WhenNpgsqlException_ShouldReturnTrue()
    {
        Exception ex = new NpgsqlException("connection reset");

        ex.IsTransientPostgresException().Should().BeTrue();
    }

    [Fact]
    public void IsTransientPostgresException_WhenGenericDbException_ShouldReturnTrue()
    {
        Exception ex = new FakeDbException();

        ex.IsTransientPostgresException().Should().BeTrue();
    }

    [Fact]
    public void IsTransientPostgresException_WhenIOException_ShouldReturnTrue()
    {
        Exception ex = new IOException("disk read failed");

        ex.IsTransientPostgresException().Should().BeTrue();
    }

    [Fact]
    public void IsTransientPostgresException_WhenTimeoutException_ShouldReturnTrue()
    {
        Exception ex = new TimeoutException("timed out");

        ex.IsTransientPostgresException().Should().BeTrue();
    }

    [Theory]
    [InlineData(typeof(InvalidOperationException))]
    [InlineData(typeof(ArgumentException))]
    [InlineData(typeof(NotSupportedException))]
    public void IsTransientPostgresException_WhenUnrelatedException_ShouldReturnFalse(Type exceptionType)
    {
        var ex = (Exception)Activator.CreateInstance(exceptionType)!;

        ex.IsTransientPostgresException().Should().BeFalse();
    }

    // A concrete DbException that is NOT an Npgsql/Postgres exception, used to exercise
    // the generic `DbException => true` branch of the switch.
    private sealed class FakeDbException : DbException
    {
        public FakeDbException() : base("fake db failure")
        {
        }
    }
}