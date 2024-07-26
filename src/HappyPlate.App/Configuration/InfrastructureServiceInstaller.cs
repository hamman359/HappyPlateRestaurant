using HappyPlate.Persistence;

using Microsoft.EntityFrameworkCore;


using Scrutor;

namespace HappyPlate.App.Configuration;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .Scan(
                selector => selector
                    .FromAssemblies(
                        Infrastructure.AssemblyReference.Assembly,
                        Persistence.AssemblyReference.Assembly)
                    .AddClasses(false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

        //services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        //services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(
            (sp, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"), default);
            });
    }
}