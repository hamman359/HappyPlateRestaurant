using System.Reflection;

using FluentValidation;

using HappyPlate.Application.Abstractions.Messaging;

using MediatR;

using NetArchTest.Rules;

namespace HappyPlate.UnitTests.ArchitectureTests;

public class ApplicationProjectArchitectureTests
    : ArchitectureTestsBase
{
    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        Assembly assembly = typeof(Application.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace,
            PersistenceNamespace,
            AppNamespace
        };

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handlers_Should_HaveDependencyOnDomain()
    {
        Assembly assembly = typeof(Application.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlers_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(Application.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Command_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(Application.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .HaveNameEndingWith("Command")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlers_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(Application.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Queries_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(Application.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            //.ImplementInterface(typeof(IQuery<>))
            //.Or()
            .ImplementInterface(typeof(ICachedQuery))
            .Should()
            .HaveNameEndingWith("Query")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEventHandlers_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(Application.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IDomainEventHandler<>))
            .Should()
            .HaveNameEndingWith("DomainEventHandler")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Validators_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(Application.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void PipelineBehaviors_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(Application.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IPipelineBehavior<,>))
            .Should()
            .HaveNameMatching("PipelineBehavior")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

}
