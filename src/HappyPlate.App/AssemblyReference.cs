using System.Reflection;

namespace HappyPlate.App;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}