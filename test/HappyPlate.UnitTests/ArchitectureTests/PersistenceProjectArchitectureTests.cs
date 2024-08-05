using System.Reflection;

using HappyPlate.Persistence.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using NetArchTest.Rules;

namespace HappyPlate.UnitTests.ArchitectureTests;

public class PersistenceProjectArchitectureTests
    : ArchitectureTestsBase
{

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

}
