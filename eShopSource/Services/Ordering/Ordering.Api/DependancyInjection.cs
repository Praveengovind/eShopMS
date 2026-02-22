namespace Ordering.Api;

public static class DependancyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication webapp)
    {
        webapp.MapControllers();

        return webapp;
    }
}
