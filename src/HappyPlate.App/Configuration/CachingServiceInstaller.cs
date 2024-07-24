using HappyPlate.Application.Caching;
using HappyPlate.Infrastructure.Caching;

namespace HappyPlate.App.Configuration;

public class CachingServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        //services.AddStackExchangeRedisCache(redisOptions =>
        //{
        //    string connection = configuration
        //        .GetConnectionString("Redis")!;

        //    redisOptions.Configuration = connection;
        //});

        services.AddMemoryCache();

        services.AddSingleton<ICacheService, CacheService>();
    }
}