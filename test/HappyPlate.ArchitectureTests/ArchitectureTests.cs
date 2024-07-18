using System.Reflection;

using FluentAssertions;

using FluentValidation;

using HappyPlate.App.Configuration;
using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Persistence.Specifications;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using NetArchTest.Rules;

namespace HappyPlate.ArchitectureTests;

public class ArchitectureTests
{
    private const string DomainNamespace = "HappyPlate.Domain";
    private const string ApplicationNamespace = "HappyPlate.Application";
    private const string InfrastructureNamespace = "HappyPlate.Infrastructure";
    private const string PresentationNamespace = "HappyPlate.Presentation";
    private const string PersistenceNamespace = "HappyPlate.Persistence";
    private const string AppNamespace = "HappyPlate.App";


    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        Assembly assembly = typeof(Domain.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            ApplicationNamespace,
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
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        Assembly assembly = typeof(Infrastructure.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
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
    public void Presentation_Should_Not_HaveDependencyOnOtherProjects()
    {
        Assembly assembly = typeof(Presentation.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            ApplicationNamespace,
            PersistenceNamespace
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
            .ImplementInterface(typeof(IQuery<>))
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
    public void Controllers_Should_HaveDependencyOnMediatR()
    {
        Assembly assembly = typeof(Presentation.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Specifications_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(Persistence.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .Inherit(typeof(Specification<>))
            .Should()
            .HaveNameEndingWith("Specification")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Interceptors_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(Persistence.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IInterceptor))
            .Should()
            .HaveNameEndingWith("Interceptor")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void EFConfigurations_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(Persistence.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IEntityTypeConfiguration<>))
            .Should()
            .HaveNameEndingWith("Configuration")
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

    [Fact]
    public void ServiceInstallers_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(App.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IServiceInstaller))
            .Should()
            .HaveNameEndingWith("ServiceInstaller")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Controllers_Should_BeNamedProperly()
    {
        Assembly assembly = typeof(Presentation.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IPipelineBehavior<,>))
            .Should()
            .HaveNameEndingWith("PipelineBehavior")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }
}
