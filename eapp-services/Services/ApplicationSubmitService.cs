using eapp_services.Models;
using Microsoft.Extensions.Caching.Memory;

namespace eapp_services.Services;

public interface IApplicationSubmitService
{
    SubmitResult? SubmitAsync(AppSubmit appSubmit);
    AppSubmit? GetAppSubmit(Guid applicationId);
}

public class ApplicationSubmitService(IMemoryCache memoryCache) : IApplicationSubmitService
{
    public AppSubmit? GetAppSubmit(Guid applicationId)
    {
        var appSubmit = memoryCache.Get<AppSubmit>(applicationId);
        return appSubmit;
    }

    public SubmitResult? SubmitAsync(AppSubmit appSubmit)
    {
        if (appSubmit == null)
        {
            return null;
        }

        var submitId = appSubmit.Id.GetValueOrDefault();
        var submittedDate = DateTime.UtcNow;
        appSubmit.SubmittedDate = submittedDate;

        memoryCache.Set(submitId, appSubmit);

        return new SubmitResult { Id = submitId, SubmitDate = submittedDate };
    }
}

