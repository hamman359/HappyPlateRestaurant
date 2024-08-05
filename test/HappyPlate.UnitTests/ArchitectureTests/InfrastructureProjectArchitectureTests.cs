using System.Reflection;

using NetArchTest.Rules;

namespace HappyPlate.UnitTests.ArchitectureTests;

public class InfrastructureProjectArchitectureTests
    : ArchitectureTestsBase
{
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
}
