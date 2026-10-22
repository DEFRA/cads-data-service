using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Validators;
using FluentAssertions;
using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Tests.Component.Validators;

public class DbAdminExecuteCommandRequestValidatorTests
{
    private readonly DbAdminExecuteCommandRequestValidator _sut = new();

    [Theory]
    [InlineData("sessions_by_state")]
    [InlineData("active_queries")]
    [InlineData("transactions")]
    [InlineData("rows_activity")]
    [InlineData("buffer_cache_hit_ratio")]
    [InlineData("locks_and_blocking")]
    [InlineData("idle_in_transaction")]
    [InlineData("prepared_transactions")]
    [InlineData("table_activity")]
    [InlineData("index_usage")]
    [InlineData("largest_tables")]
    [InlineData("slow_queries")]
    [InlineData("shared_block_reads")]
    [InlineData("database_size")]
    [InlineData("reset_pg_stat_statements")]
    public void GivenAllowedCommandWithoutArgs_WhenValidated_ShouldBeValid(string command)
    {
        var result = _sut.Validate(new DbAdminExecuteCommandRequest(command, null));

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("database_size")]
    [InlineData("reset_pg_stat_statements")]
    public void GivenNewCommandWithIgnoredArgs_WhenValidated_ShouldBeValid(string command)
    {
        var result = _sut.Validate(new DbAdminExecuteCommandRequest(command, Parse("""{"pid":"ignored"}""")));

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("not_a_real_command")]
    [InlineData("DATABASE_SIZE")]
    public void GivenUnknownCommand_WhenValidated_ShouldBeInvalid(string command)
    {
        var result = _sut.Validate(new DbAdminExecuteCommandRequest(command, null));

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("cancel_query", """{"pid":1}""")]
    [InlineData("cancel_query", """{"pid":12345}""")]
    [InlineData("cancel_query", """{"pid":2147483647}""")]
    [InlineData("terminate_query", """{"pid":1}""")]
    [InlineData("terminate_query", """{"pid":2147483647,"other":"ignored"}""")]
    public void GivenPidCommandWithValidPid_WhenValidated_ShouldBeValid(string command, string argsJson)
    {
        var result = _sut.Validate(new DbAdminExecuteCommandRequest(command, Parse(argsJson)));

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("cancel_query")]
    [InlineData("terminate_query")]
    public void GivenPidCommandWithNoArgs_WhenValidated_ShouldBeInvalidOnArgs(string command)
    {
        var result = _sut.Validate(new DbAdminExecuteCommandRequest(command, null));

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Args");
    }

    [Theory]
    [MemberData(nameof(InvalidPidArgs))]
    public void GivenPidCommandWithInvalidPid_WhenValidated_ShouldBeInvalidOnArgs(string command, string argsJson)
    {
        var result = _sut.Validate(new DbAdminExecuteCommandRequest(command, Parse(argsJson)));

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e =>
            e.PropertyName == "Args"
            && e.ErrorMessage == "Args must contain an integer 'pid' property between 1 and 2147483647.");
    }

    public static TheoryData<string, string> InvalidPidArgs()
    {
        string[] argsCases =
        [
            "{}",                       // missing
            """{"process_id":123}""",   // wrong property
            """{"pid":null}""",         // null
            """{"pid":"123"}""",        // string
            """{"pid":true}""",         // boolean
            """{"pid":12.5}""",         // non-integer
            """{"pid":1.0}""",          // non-integer literal
            """{"pid":1e3}""",          // exponent literal
            """{"pid":0}""",            // zero
            """{"pid":-1}""",           // negative
            """{"pid":2147483648}""",   // above int range
            "null",                     // JSON null args
            "[123]",                    // array args
            "123"                       // scalar args
        ];

        var data = new TheoryData<string, string>();
        foreach (var command in new[] { "cancel_query", "terminate_query" })
        {
            foreach (var args in argsCases)
            {
                data.Add(command, args);
            }
        }

        return data;
    }

    private static JsonElement Parse(string json)
    {
        using var document = JsonDocument.Parse(json);
        return document.RootElement.Clone();
    }
}