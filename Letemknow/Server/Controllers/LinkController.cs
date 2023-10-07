using Letemknow.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Letemknow.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class LinkController : Controller
{
    public LinkController(IDbContextFactory<LEKContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    [HttpGet()]
    public async Task<MailLink> GetLink(string id, CancellationToken cancellationToken)
    {
        using var ctx = await _contextFactory.CreateDbContextAsync(cancellationToken);
        return await ctx.MailLinks.FindAsync(id) ?? throw new KeyNotFoundException();
    }

    [HttpGet("list")]
    public async Task<IEnumerable<MailLink>> GetAllLinks(CancellationToken cancellationToken)
    {
        using var ctx = await _contextFactory.CreateDbContextAsync(cancellationToken);
        return await ctx.MailLinks.ToListAsync();
    }

    [HttpPost]
    public async Task<MailLink> Post(MailLink link, CancellationToken cancellationToken)
    {
        link.Created = DateTimeOffset.UtcNow;
        using var ctx = await _contextFactory.CreateDbContextAsync(cancellationToken);
        ctx.MailLinks.Add(link);
        await ctx.SaveChangesAsync();
        return link;
    }

    private readonly IDbContextFactory<LEKContext> _contextFactory;


}
