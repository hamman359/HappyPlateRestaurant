using System.Reflection;

using HappyPlate.App.Configuration;

using NetArchTest.Rules;

namespace HappyPlate.UnitTests.ArchitectureTests;

public class AppProjectArchitectureTests
    : ArchitectureTestsBase
{

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

}
