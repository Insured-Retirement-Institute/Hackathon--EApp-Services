using eapp_services.Models;
using eapp_services.Services;
using Microsoft.AspNetCore.Mvc;

namespace eapp_services.Controllers;

[ApiController]
[Route("application")]
public class ApplicationController(IApplicationService applicationService, IApplicationSubmitService submitService) : ControllerBase
{
    [HttpGet("template/{templateId}")]
    public async Task<App> GetTemplate(Guid templateId)
    {
        var newApp = await applicationService.GetAppInstance(templateId);
        return newApp;
    }

    [HttpGet("templates")]
    public async Task<AppTemplate[]> GetTemplates()
    {
        var appTemplates = await applicationService.GetTemplates();
        return appTemplates;
    }

    [HttpPost("submit")]
    public IActionResult Submit(AppSubmit? appSubmit)
    {
        if (appSubmit == null)
        {
            return BadRequest("Data not present");
        }

        if (appSubmit?.Id == null)
        {
            return BadRequest("Application ID is missing");
        }

        if (appSubmit?.DataItems == null || appSubmit?.DataItems?.Length == 0)
        {
            return BadRequest("Data Items are missing");
        }

        var result = submitService.SubmitAsync(appSubmit);
        return Ok(result);
    }

    [HttpGet("submit/{applicationId}")]
    public AppSubmit GetSubmit(Guid applicationId)
    {
        var appSubmit = submitService.GetAppSubmit(applicationId);
        return appSubmit;
    }
}
