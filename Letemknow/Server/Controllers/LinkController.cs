using Letemknow.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web;

namespace Letemknow.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class LinkController : Controller
{
    public LinkController(IDbContextFactory<LEKContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    [HttpGet("/{id}")]
    public async Task<IActionResult> GetLink(string id, CancellationToken cancellationToken)
    {
        using var ctx = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var link = await ctx.MailLinks.FindAsync(id);
        return Redirect(CreateLinkUri(link));
    }

    [HttpPost]
    public async Task<MailLink> Post(MailLink link, CancellationToken cancellationToken)
    {
        using var ctx = await _contextFactory.CreateDbContextAsync(cancellationToken);
        ctx.MailLinks.Add(link);
        await ctx.SaveChangesAsync();
        var allofem = await ctx.MailLinks.ToListAsync();
        return link;
    }

    private string CreateLinkUri(MailLink link) =>
        $"mailto:{link.Recipient}?subject={WebUtility.UrlEncode(link.Subject)}&body={WebUtility.UrlEncode(link.Body)}";

    private readonly IDbContextFactory<LEKContext> _contextFactory;


}
