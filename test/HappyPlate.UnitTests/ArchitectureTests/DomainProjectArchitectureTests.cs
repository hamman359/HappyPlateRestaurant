using System.Reflection;

using HappyPlate.Domain.Primatives;

using NetArchTest.Rules;

namespace HappyPlate.UnitTests.ArchitectureTests;

public class DomainProjectArchitectureTests
    : ArchitectureTestsBase
{
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
    public void ValueObjectClasses_Should_InheritFrom_ValueObject()
    {
        Assembly assembly = typeof(Domain.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ResideInNamespace($"{DomainNamespace}.ValueObjects")
            .Should()
            .Inherit(typeof(ValueObject))
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void EntityClasses_Should_InheritFrom_Entity()
    {
        Assembly assembly = typeof(Domain.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ResideInNamespaceStartingWith($"{DomainNamespace}.Entities")
            .Should()
            .Inherit(typeof(Entity))
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainErrorClasses_Should_BeStatic()
    {
        Assembly assembly = typeof(Domain.AssemblyReference).Assembly;

        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ResideInNamespaceStartingWith($"{DomainNamespace}.Errors")
            .Should()
            .BeStatic()
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }
}
