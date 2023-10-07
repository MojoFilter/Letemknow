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

    // GET: api/<TrackController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<TrackController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<TrackController>
    [HttpPost]
    public async Task Post(MailLinkClick click, CancellationToken cancellationToken)
    {
        click.Timestamp = DateTimeOffset.UtcNow;
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

    // PUT api/<TrackController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<TrackController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

    private readonly IDbContextFactory<LEKContext> _contextFactory;

}
