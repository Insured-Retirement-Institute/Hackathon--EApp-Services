using eapp_services.Models;
using eapp_services.Services;
using Microsoft.AspNetCore.Mvc;

namespace eapp_services.Controllers;

[ApiController]
[Route("application")]
public class ApplicationController(IApplicationService applicationService) : ControllerBase
{
    [HttpGet("template/{templateId}")]
    public async Task<App> Template(Guid templateId)
    {
        var newApp = await applicationService.GetAppInstance(templateId);
        return newApp;
    }
}
