using Letemknow.Shared;
using Microsoft.EntityFrameworkCore;

namespace Letemknow.Server;

public class LEKContext : DbContext
{
    public LEKContext(DbContextOptions<LEKContext> opt) : base(opt)
    {
    }

    public DbSet<MailLink> MailLinks { get; set; }

    public DbSet<MailLinkClick> Clicks { get; set; }
}
