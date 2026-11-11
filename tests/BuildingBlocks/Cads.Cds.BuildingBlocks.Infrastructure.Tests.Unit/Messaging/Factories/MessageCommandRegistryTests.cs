using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Commands;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Messaging.Factories;

public class MessageCommandRegistryTests
{
    private readonly MessageCommandRegistry<TestQueueClient> _registry = new();

    private const string Subject = "TestSubject";
    private const string OtherSubject = "OtherSubject";

    [Fact]
    public void Register_ShouldStoreFactoryAgainstSubject()
    {
        _registry.Register<FirstCommandFactory>(Subject);

        var command = _registry.CreateCommand(NewMessage(Subject));

        command.Should().BeOfType<FirstCommand>();
    }

    [Fact]
    public void Register_ShouldReturnRegistry_ForChaining()
    {
        var result = _registry.Register<FirstCommandFactory>(Subject);

        result.Should().BeSameAs(_registry);
    }

    [Fact]
    public void CreateCommand_ShouldThrowNonRetryable_WhenSubjectNotRegistered()
    {
        var act = () => _registry.CreateCommand(NewMessage("UnknownSubject"));

        act.Should()
            .Throw<NonRetryableException>()
            .WithMessage("No command registered for subject UnknownSubject");
    }

    [Fact]
    public void CreateCommand_ShouldBeCaseSensitiveOnSubject()
    {
        _registry.Register<FirstCommandFactory>(Subject);

        var act = () => _registry.CreateCommand(NewMessage(Subject.ToLowerInvariant()));

        act.Should().Throw<NonRetryableException>();
    }

    [Fact]
    public void CreateCommand_ShouldPassUnwrappedMessageToFactory()
    {
        _registry.Register<FirstCommandFactory>(Subject);

        var message = new UnwrappedMessage
        {
            Subject = Subject,
            MessageId = "ABC",
            Payload = "{\"test\":1}",
            CorrelationId = "Corr-123"
        };

        var command = (FirstCommand)_registry.CreateCommand(message);

        command.Message.Should().BeSameAs(message);
    }

    [Fact]
    public void Register_ShouldOverrideExistingFactory_WhenSameSubjectIsUsed()
    {
        _registry.Register<FirstCommandFactory>(Subject);
        _registry.Register<SecondCommandFactory>(Subject);

        _registry.CreateCommand(NewMessage(Subject)).Should().BeOfType<SecondCommand>();
    }

    [Fact]
    public void Registry_ShouldSupportMultipleSubjects()
    {
        _registry
            .Register<FirstCommandFactory>(Subject)
            .Register<SecondCommandFactory>(OtherSubject);

        _registry.CreateCommand(NewMessage(Subject)).Should().BeOfType<FirstCommand>();
        _registry.CreateCommand(NewMessage(OtherSubject)).Should().BeOfType<SecondCommand>();
    }

    private static UnwrappedMessage NewMessage(string subject) => new()
    {
        Subject = subject,
        MessageId = Guid.NewGuid().ToString(),
        Payload = "{}"
    };

    public sealed class TestQueueClient : IQueueClient
    {
        public string ClientName => nameof(TestQueueClient);
    }

    private sealed class FirstCommandFactory : IMessageCommandFactory
    {
        public IMessageProcessingCommand Create(UnwrappedMessage message) => new FirstCommand(message);
    }

    private sealed class SecondCommandFactory : IMessageCommandFactory
    {
        public IMessageProcessingCommand Create(UnwrappedMessage message) => new SecondCommand(message);
    }

    private sealed record FirstCommand(UnwrappedMessage Message) : IMessageProcessingCommand;

    private sealed record SecondCommand(UnwrappedMessage Message) : IMessageProcessingCommand;
}
