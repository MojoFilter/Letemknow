using Letemknow.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Letemknow.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class TrackController : ControllerBase
{
    public TrackController(IDbContextFactory<LEKContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    [HttpPost]
    public async Task Post(MailLinkClick click, CancellationToken cancellationToken)
    {
        click.Timestamp = DateTimeOffset.UtcNow;
        click.Ip = HttpContext.Connection.RemoteIpAddress;
        using var ctx = await _contextFactory.CreateDbContextAsync();
        ctx.Clicks.Add(click);
        try
        {
            ctx.SaveChanges();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    private readonly IDbContextFactory<LEKContext> _contextFactory;

}
