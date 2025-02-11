using eapp_services.Models;

namespace eapp_services.Services;

public interface IApplicationService
{
    Task<App> GetAppInstance(Guid templateId);
}

public class ApplicationService : IApplicationService
{
    public Task<App> GetAppInstance(Guid templateId)
    {
        var newApp = new App
        {
            Id = Guid.NewGuid()
        };

        return Task.FromResult(newApp);
    }
}
