using Letemknow.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Letemknow.Server.Controllers;

[Route("api/[controller]")]
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

    [HttpGet("{linkId}/{target}")]
    public async Task<IEnumerable<DateClicks>> GetClicksByDate(string linkId, ClickTarget target, DateTime startDate, DateTime endDate, CancellationToken token)
    {
        using var ctx = await _contextFactory.CreateDbContextAsync(token);
        return (await ctx.Clicks
            .Where(c => c.LinkId == linkId
                                && c.Target == target)
                                //&& c.Timestamp >= startDate
                                //&& c.Timestamp <= endDate)
            .ToListAsync())
            .GroupBy(c => c.Timestamp.Date)
            .Select(g => new DateClicks(DateOnly.FromDateTime(g.Key), g.Count()))
            .ToList();
    }

    [HttpGet("{linkId}")]
    public async Task<ClickStatData> GetLinkStats(string linkId, CancellationToken cancellationToken)
    {
        using var ctx = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var stats = new ClickStatData();
        stats.TotalClicks = await ctx.Clicks.Where(c => c.LinkId == linkId).CountAsync();
        stats.TargetClicks = await this.TargetClicksAsync(linkId, cancellationToken);
        return stats;
    }

    private Task<TargetClickCount[]> TargetClicksAsync(string linkId, CancellationToken cancellationToken) =>
       Task.WhenAll(
        GetTargetClicksAsync(linkId, ClickTarget.MailClient, cancellationToken),
            GetTargetClicksAsync(linkId, ClickTarget.Gmail, cancellationToken),
            GetTargetClicksAsync(linkId, ClickTarget.Outlook, cancellationToken),
            GetTargetClicksAsync(linkId, ClickTarget.Yahoo, cancellationToken));
    

    private async Task<TargetClickCount> GetTargetClicksAsync(string linkId, ClickTarget target, CancellationToken cancellationToken)
    {
        using var ctx = await _contextFactory.CreateDbContextAsync();
        return new(target,
          await ctx.Clicks.Where(c => c.LinkId == linkId && c.Target == target).CountAsync(cancellationToken));
    }

    private readonly IDbContextFactory<LEKContext> _contextFactory;

}
