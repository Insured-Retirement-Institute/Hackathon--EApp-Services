using eapp_services.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace eapp_services.Services;

public interface IApplicationService
{
    Task<AppTemplate[]?> GetTemplates();
    Task<App?> GetAppInstance(Guid templateId);
}

public class ApplicationService : IApplicationService
{
    private readonly IWebHostEnvironment hostingEnvironment;
    private readonly IMemoryCache memoryCache;

    public ApplicationService(IWebHostEnvironment hostingEnvironment, IMemoryCache memoryCache)
    {
        this.hostingEnvironment = hostingEnvironment;
        this.memoryCache = memoryCache;
    }

    public async Task<App?> GetAppInstance(Guid templateId)
    {
        var appTemplates = await LoadApplicationsFromFile();
        var appTemplate = appTemplates.FirstOrDefault(app => app.Id == templateId);
        if (appTemplate == null)
        {
            return null;
        }

        return PrepareApp(appTemplate);
    }

    private static App PrepareApp(App appTemplate)
    {
        var applicationId = Guid.NewGuid();
        var callbackUrl = $"application/submit";

        appTemplate.Id = applicationId;
        appTemplate.CallbackUrl = callbackUrl;

        var stages = appTemplate.Stages;
        foreach (var stage in stages)
        {
            var items = stage.DataItems;
            foreach (var item in items)
            {
                if (item.DataItemId is null)
                {
                    item.DataItemId = Guid.NewGuid();
                }
            }

            stage.DataItems = items.OrderBy(item => item.Order).ToArray();
        }

        appTemplate.Stages = stages.OrderBy(item => item.Order).ToArray();

        return appTemplate;
    }

    private async Task<App[]> LoadApplicationsFromFile()
    {
        App[] applications = memoryCache.Get<App[]>("__applications__");
        if (applications != null)
        {
            return applications;
        }

        var contentPath = hostingEnvironment.ContentRootPath;
        var appDataPath = Path.Combine(contentPath, "App_Data");

        var temp = new List<App>();
        var appFiles = Directory.GetFiles(appDataPath, "*.json");

        foreach (var appFile in appFiles)
        {
            var appTemplate = await DeserializeAppAsync(appFile);
            if (appTemplate == null) continue;

            temp.Add(appTemplate);
        }

        applications = temp.ToArray();
        memoryCache.Set("__applications__", applications);
        return applications;
    }

    private async Task<App?> DeserializeAppAsync(string file)
    {
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        using FileStream openStream = File.OpenRead(file);
        var appTemplate = await JsonSerializer.DeserializeAsync<App>(openStream, options);

        return appTemplate;
    }

    public async Task<AppTemplate[]?> GetTemplates()
    {
        var appTemplates = await LoadApplicationsFromFile();
        var templates = appTemplates.Select(t => new AppTemplate { Id = t.Id, Name = t.Name, });
        return templates.ToArray();
    }
}
