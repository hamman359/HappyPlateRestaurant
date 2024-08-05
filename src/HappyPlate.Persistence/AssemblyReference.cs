using System.Reflection;

namespace HappyPlate.Persistence;

/// <summary>
/// Provides strongly typed reference to this assembly
/// </summary>
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}