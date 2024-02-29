using Microsoft.EntityFrameworkCore;
using RestaurantService.Infra.Data;
using RestaurantService.Infra.Repository;

namespace RestaurantService.Extensions;
public static class AppExtensions
{
    public static IServiceCollection AddAppExtensions(this IServiceCollection services, IConfiguration config)
    {
        var databaseConnectionString = config.GetConnectionString("databaseConnectionStringRestaurantServiceApi");

        services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(databaseConnectionString, ServerVersion.AutoDetect(databaseConnectionString)));

        services.AddScoped<IRestaurantRepository, RestaurantRepository>();

        return services;
    }
}
