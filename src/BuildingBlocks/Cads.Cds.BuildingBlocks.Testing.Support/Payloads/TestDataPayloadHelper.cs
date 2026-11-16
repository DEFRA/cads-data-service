using System.Text.Json;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Payloads;

public static class TestDataPayloadHelper
{
    public const string EmptyJsonObject = "{}";
    public const string InvalidJsonPayload = "{ }";
    public const string SamplePayload = "{\"foo\":1}";
    public const string SampleStringPayload = "{\"foo\":\"bar\"}";
    public const string SqsMessagePayload = "{\"id\":\"00000000-0000-0000-0000-000000000001\", \"message\":\"Test message 1\"}";
    public const string SampleJsonPayload = "{\"test\":1}";
    public const string SerializedSqsMessageBody = "serialized message";
    public const string SqsDeadMessageBody = "dead";
    public const string SqsMessageBody1 = "b1";
    public const string SqsMessageBody2 = "b2";
    public const string SqsMessageBody3 = "b3";

    public static string AnimalMovementV1 => ReadEmbeddedPayload("AnimalMovementV1.json");

    public static string AnimalMovementV2 => ReadEmbeddedPayload("AnimalMovementV2.json");

    public static string CreateTestPayload(string testId) =>
        JsonSerializer.Serialize(new { test = testId });

    public static string CreateReportJobRequestedPayload(string reportJobId, string reportKey) =>
        JsonSerializer.Serialize(new { reportJobId, reportKey });

    private static string ReadEmbeddedPayload(string fileName)
    {
        var resourceName = $"{typeof(TestDataPayloadHelper).Namespace}.{fileName}";
        using var stream = typeof(TestDataPayloadHelper).Assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded test payload '{resourceName}' was not found.");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}