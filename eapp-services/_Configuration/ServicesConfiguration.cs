using eapp_services.Services;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IApplicationService, ApplicationService>();

        return services;
    }
}