using System.Reflection;

using MediatR;

using NetArchTest.Rules;

namespace HappyPlate.UnitTests.ArchitectureTests;

public class PresentationProjectArchitectureTests
    : ArchitectureTestsBase
{
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
