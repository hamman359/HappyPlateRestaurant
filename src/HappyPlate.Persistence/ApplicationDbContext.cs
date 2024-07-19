using Microsoft.EntityFrameworkCore;

namespace HappyPlate.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    //Update-Database -a HappyPlate.Persistence -s HappyPlate.App
    //Add-Migration InitialCreate -a HappyPlate.Persistence -s HappyPlate.App
}