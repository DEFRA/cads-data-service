using Cads.Cds.BuildingBlocks.Application.Messaging.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.Payloads;
using Cads.Cds.StorageBridge.Application.Messaging.Commands;
using Cads.Cds.StorageBridge.Infrastructure.Messaging.Factories;
using FluentAssertions;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.Messaging.Factories;

public class StorageBridgeMessageCommandRegistryTests
{
    private readonly StorageBridgeMessageCommandRegistry _registry = new();

    private const string Subject = "S3ToPostgresCopy";
    private const string OtherSubject = "OtherMessage";

    [Fact]
    public void Register_ShouldStoreFactoryAgainstSubject()
    {
        _registry.Register<S3ToPostgresCopyMessageCommandFactory>(Subject);

        var message = new UnwrappedMessage
        {
            Subject = Subject,
            MessageId = "123",
            Payload = TestDataPayloadHelper.EmptyJsonObject
        };

        var command = _registry.CreateCommand(message);

        command.Should().BeOfType<ProcessS3ToPostgresCopyMessageCommand>();
    }

    [Fact]
    public void CreateCommand_ShouldThrow_WhenSubjectNotRegistered()
    {
        var message = new UnwrappedMessage
        {
            Subject = "UnknownSubject",
            MessageId = "123",
            Payload = TestDataPayloadHelper.EmptyJsonObject
        };

        var act = () => _registry.CreateCommand(message);

        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("No command registered for subject UnknownSubject");
    }

    [Fact]
    public void CreateCommand_ShouldPassUnwrappedMessageToFactory()
    {
        _registry.Register<S3ToPostgresCopyMessageCommandFactory>(Subject);

        var message = new UnwrappedMessage
        {
            Subject = Subject,
            MessageId = "ABC",
            Payload = TestDataPayloadHelper.SampleJsonPayload,
            CorrelationId = "Corr-123"
        };

        var command = _registry.CreateCommand(message);

        command.Should().BeOfType<ProcessS3ToPostgresCopyMessageCommand>();

        var typed = (ProcessS3ToPostgresCopyMessageCommand)command;
        typed.Message.MessageId.Should().Be("ABC");
        typed.Message.Payload.Should().Be(TestDataPayloadHelper.SampleJsonPayload);
        typed.Message.CorrelationId.Should().Be("Corr-123");
    }

    [Fact]
    public void Register_ShouldOverrideExistingFactory_WhenSameSubjectIsUsed()
    {
        _registry.Register<S3ToPostgresCopyMessageCommandFactory>(Subject);
        _registry.Register<TestMessageCommandFactory>(Subject); // override

        var message = new UnwrappedMessage
        {
            Subject = Subject,
            MessageId = "XYZ",
            Payload = TestDataPayloadHelper.EmptyJsonObject
        };

        var command = _registry.CreateCommand(message);

        command.Should().BeOfType<TestMessageProcessingCommand>();
    }

    [Fact]
    public void Registry_ShouldSupportMultipleSubjects()
    {
        _registry.Register<S3ToPostgresCopyMessageCommandFactory>(Subject);
        _registry.Register<TestMessageCommandFactory>(OtherSubject);

        var csvMessage = new UnwrappedMessage { Subject = Subject, MessageId = "1", Payload = TestDataPayloadHelper.EmptyJsonObject };
        var otherMessage = new UnwrappedMessage { Subject = OtherSubject, MessageId = "2", Payload = TestDataPayloadHelper.EmptyJsonObject };

        _registry.CreateCommand(csvMessage).Should().BeOfType<ProcessS3ToPostgresCopyMessageCommand>();
        _registry.CreateCommand(otherMessage).Should().BeOfType<TestMessageProcessingCommand>();
    }

    private sealed class TestMessageCommandFactory : IMessageCommandFactory
    {
        public IMessageProcessingCommand Create(UnwrappedMessage message)
            => new TestMessageProcessingCommand(message);
    }

    private sealed class TestMessageProcessingCommand(UnwrappedMessage message) : IMessageProcessingCommand
    {
        public UnwrappedMessage Message { get; } = message;

        public static Task ExecuteAsync()
            => Task.CompletedTask;
    }
}