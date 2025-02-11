using eapp_services.Services;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddMemoryCache();
        services.AddTransient<IApplicationService, ApplicationService>();
        services.AddTransient<IApplicationSubmitService, ApplicationSubmitService>();

        return services;
    }
}