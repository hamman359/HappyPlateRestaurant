﻿namespace HappyPlate.App.Configuration;

public class PresentationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);

        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
        });
    }
}